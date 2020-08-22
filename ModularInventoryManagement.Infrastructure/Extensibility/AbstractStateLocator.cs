using System;
using System.Collections.Generic;
using System.Text;

namespace ModularInventoryManagement.Infrastructure.Extensibility
{
    public abstract class AbstractStateLocator : IStateLocator
    {
        private IStateObject State;
        private bool CanSetState = true;

        public AbstractStateLocator() { }


        public IStateLocator SetInitialState(IStateObject initialState)
        {
            if (!CanSetState)
            {
                throw new FieldAccessException("Field cannot be re-set!");
            }
            State = initialState;
            CanSetState = false;
            return this;
        }

        public T Get<T>(string key) where T : class
        {
            return State.GetState<T>(key);
        }

        public void Put<T>(string key, T data) where T : class
        {
            State.PutState(key, data);
        }

        public void CopyTo(IStateObject target) => State.CopyTo(target);

        public void Clear()
        {
            State.Clear();
            State.PutState("isLogin", "true");
        }
    }
}
