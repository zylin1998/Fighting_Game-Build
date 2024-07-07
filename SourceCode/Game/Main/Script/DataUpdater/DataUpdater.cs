using System.Collections;
using System.Collections.Generic;

namespace FightingGame
{
    public class DataUpdater
    {
        public DataUpdater() 
        {
            Contexts = new();
        }

        public Dictionary<object, IUpdateContext> Contexts { get; }

        public void Register(IUpdateContext context) 
        {
            Contexts.Add(context.Id, context);
        }

        public bool Unregister(object id)
        {
            return Contexts.Remove(id);
        }

        public void Update(object id, object value) 
        {
            var exist = Contexts.TryGetValue(id, out var context);

            if (exist) 
            {
                context.SetContext(value);
            }
        }
    }

    public interface IUpdateContext 
    {
        public object Id { get; }

        public void SetContext(object value);
    }

    public interface IUpdateGroup 
    {
        public IEnumerable<IUpdateContext> Contexts { get; }
    }
}