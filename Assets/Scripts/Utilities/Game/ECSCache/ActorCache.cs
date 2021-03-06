﻿// // --------------------------------------------------------------------------------------------------------------------
// // <copyright file="ActorCache.cs" author="Lars" company="None">
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), 
// to deal in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software,
// and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
// PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
// FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// // </copyright>
// // <summary>
// //   TODO - Insert file description
// // </summary>
// // --------------------------------------------------------------------------------------------------------------------

namespace Utilities.Game.ECSCache
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using ECS.Entities;
    using ECS.Entities.Blueprint;

    public class ActorCache : MonoSingleton<ActorCache>, ICache<IActor, IEntityBlueprint>
    {
        private readonly Dictionary<IEntityBlueprint, List<IActor>> _actorsByType = new Dictionary<IEntityBlueprint, List<IActor>>();

        private readonly List<IActor> _actors = new List<IActor>();

        public IActor Player { get; private set; }

        public void Add(IActor actor, IEntityBlueprint blueprint)
        {
            if (_actors.Contains(actor))
                throw new InvalidOperationException("Actors already contains this actor");
            _actors.Add(actor);

            if (blueprint is Player)
            {
                Player = actor;
                return;
            }

            if (_actorsByType.ContainsKey(blueprint))
            {
                _actorsByType[blueprint].Add(actor);
            }
            else
            {
                _actorsByType.Add(blueprint, new List<IActor> { actor });
            }
        }

        public void Remove(IActor actor)
        {
            if (!_actors.Contains(actor))
                throw new InvalidOperationException("Actors does not contain this actor");

            _actors.Remove(actor);

            // TODO - Optimize?
            foreach (var pair in _actorsByType)
            {
                var actors = pair.Value;
                if (!actors.Contains(actor)) continue;

                actors.Remove(actor);
                break;
            }
        }

        public bool Contains(IActor actor)
        {
            return _actors.Contains(actor);
        }

        public IEnumerable<IActor> GetCached()
        {
            return _actors.AsReadOnly();
        }

        public ReadOnlyCollection<IActor> GetCached(IEntityBlueprint type)
        {
            return _actorsByType.ContainsKey(type) ? _actorsByType[type].AsReadOnly() : null;
        }

        public IEnumerable<T> GetCached<T>()
            where T : class, IActor
        {
            throw new NotImplementedException();
        }
    }
}