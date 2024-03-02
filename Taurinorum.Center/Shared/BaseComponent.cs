using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using Taurinorum.Object.DataTrasportObject;

namespace Taurinorum.Center.Shared
{
    public abstract class BaseComponent : IComponent, IHandleEvent, IHandleAfterRender
    {
        protected RenderFragment _renderFragment;
        private RenderHandle _renderHandle;
        protected bool _initialized;
        protected bool _hasNeverRendered = true;
        protected bool _hasPendingQueuedRender;
        protected bool _hasCalledOnAfterRender;

        public BaseComponent()
        {
            _renderFragment = builder =>
            {
                _hasPendingQueuedRender = false;
                _hasNeverRendered = false;
                BuildRenderTree(builder);
            };
        }

        public UtenteDTO utente = new UtenteDTO();
        protected virtual void BuildRenderTree(RenderTreeBuilder builder) { }
        protected virtual void OnInitialized() { }
        protected virtual Task OnInitializedAsync() => Task.CompletedTask;
        protected virtual void OnParametersSet() { }
        protected virtual Task OnParametersSetAsync() => Task.CompletedTask;
        protected virtual bool ShouldRender() => true;
        protected virtual void OnAfterRender(bool firstRender) { }
        protected virtual Task OnAfterRenderAsync(bool firstRender) => Task.CompletedTask;
        protected Task InvokeAsync(Action workItem) => _renderHandle.Dispatcher.InvokeAsync(workItem);
        protected Task InvokeAsync(Func<Task> workItem) => _renderHandle.Dispatcher.InvokeAsync(workItem);

        protected void StateHasChanged()
        {
            if (_hasPendingQueuedRender)
                return;

            if (_hasNeverRendered || ShouldRender() || _renderHandle.IsRenderingOnMetadataUpdate)
            {
                _hasPendingQueuedRender = true;

                try
                {
                    _renderHandle.Render(_renderFragment);
                }
                catch
                {
                    _hasPendingQueuedRender = false;
                    throw;
                }
            }
        }

        void IComponent.Attach(RenderHandle renderHandle)
        {
            if (_renderHandle.IsInitialized)
                throw new InvalidOperationException($"The render handle is already set. Cannot initialize a {nameof(ComponentBase)} more than once.");

            _renderHandle = renderHandle;
        }

        public virtual Task SetParametersAsync(ParameterView parameters)
        {
            parameters.SetParameterProperties(this);

            if (!_initialized)
            {
                _initialized = true;

                return RunInitAndSetParametersAsync();
            }
            else
                return CallOnParametersSetAsync();
        }

        private async Task RunInitAndSetParametersAsync()
        {
            OnInitialized();

            var task = OnInitializedAsync();

            if (task.Status != TaskStatus.RanToCompletion && task.Status != TaskStatus.Canceled)
            {
                StateHasChanged();

                try
                {
                    await task;
                }
                catch // avoiding exception filters for AOT runtime support
                {
                    if (!task.IsCanceled)
                        throw;
                }
            }

            await CallOnParametersSetAsync();
        }

        private Task CallOnParametersSetAsync()
        {
            OnParametersSet();

            var task = OnParametersSetAsync();
            var shouldAwaitTask = task.Status != TaskStatus.RanToCompletion &&
                task.Status != TaskStatus.Canceled;

            StateHasChanged();

            return shouldAwaitTask ?
                CallStateHasChangedOnAsyncCompletion(task) :
                Task.CompletedTask;
        }

        private async Task CallStateHasChangedOnAsyncCompletion(Task task)
        {
            try
            {
                await task;
            }
            catch
            {
                if (task.IsCanceled)
                    return;

                throw;
            }
            StateHasChanged();
        }
        public string GetEnumDisplayName(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var displayAttribute = (DisplayAttribute)Attribute.GetCustomAttribute(field, typeof(DisplayAttribute));

            return displayAttribute?.Name ?? value.ToString();
        }

        Task IHandleEvent.HandleEventAsync(EventCallbackWorkItem callback, object? arg)
        {
            var task = callback.InvokeAsync(arg);
            var shouldAwaitTask = task.Status != TaskStatus.RanToCompletion && task.Status != TaskStatus.Canceled;

            StateHasChanged();

            return shouldAwaitTask ?
                CallStateHasChangedOnAsyncCompletion(task) :
                Task.CompletedTask;
        }

        Task IHandleAfterRender.OnAfterRenderAsync()
        {
            var firstRender = !_hasCalledOnAfterRender;
            _hasCalledOnAfterRender |= true;

            OnAfterRender(firstRender);

            return OnAfterRenderAsync(firstRender);
        }
    }
}
