using System;
using System.Collections.Generic;
using TFT_Friendly.Back.Exceptions;
using TFT_Friendly.Back.Models.Sets;
using TFT_Friendly.Back.Services.Mongo;

namespace TFT_Friendly.Back.Services.Sets
{
    /// <summary>
    /// SetService class
    /// </summary>
    public class SetService : ISetService
    {
        #region MEMBERS

        private readonly SetsContext _setsContext;

        #endregion MEMBERS

        #region CONSTRUCTOR

        /// <summary>
        /// Initialize a new <see cref="SetService"/> class
        /// </summary>
        /// <param name="setsContext">The sets context to use</param>
        /// <exception cref="ArgumentNullException">Throw an exception if one parameter is null</exception>
        public SetService(SetsContext setsContext)
        {
            _setsContext = setsContext ?? throw new ArgumentNullException(nameof(setsContext));
        }

        #endregion CONSTRUCTOR

        #region METHODS

        /// <summary>
        /// Get a list of sets
        /// </summary>
        /// <returns>The list of sets</returns>
        public List<Set> GetSetList()
        {
            return _setsContext.GetSets();
        }

        /// <summary>
        /// Get a specific set
        /// </summary>
        /// <param name="key">The key of the set</param>
        /// <returns>The requested set</returns>
        /// <exception cref="SetNotFoundException">Throw an exception if set doesn't exist</exception>
        public Set GetSet(string key)
        {
            if (!_setsContext.IsSetExist(key))
                throw new SetNotFoundException("Set not found");
            return _setsContext.GetSet(key);
        }

        /// <summary>
        /// Add a new set
        /// </summary>
        /// <param name="set">The set to add</param>
        /// <returns>The newly added set</returns>
        /// <exception cref="SetConflictException">Throw an exception if a set with the same key already exist</exception>
        public Set AddSet(Set set)
        {
            if (_setsContext.IsSetExist(set.Key))
                throw new SetConflictException("A set with this key already exist");
            return _setsContext.AddSet(set);
        }

        /// <summary>
        /// Update a set
        /// </summary>
        /// <param name="set">The set to update</param>
        /// <returns>The updated set</returns>
        /// <exception cref="SetNotFoundException">Throw an exception if the set doesn't exist</exception>
        public Set UpdateSet(Set set)
        {
            if (!_setsContext.IsSetExist(set.Key))
                throw new SetNotFoundException("Set not found");
            return _setsContext.UpdateSet(set);
        }

        /// <summary>
        /// Update a set
        /// </summary>
        /// <param name="key">The key of the set to update</param>
        /// <param name="set">The set to update</param>
        /// <returns>The updated set</returns>
        /// <exception cref="SetNotFoundException">Throw an exception if the set doesn't exist</exception>
        public Set UpdateSet(string key, Set set)
        {
            if (!_setsContext.IsSetExist(key))
                throw new SetNotFoundException("Set not found");
            return _setsContext.UpdateSet(key, set);
        }

        /// <summary>
        /// Delete a set
        /// </summary>
        /// <param name="key">The key of set to delete</param>
        /// <exception cref="SetNotFoundException">Throw an exception if the set doesn't exist</exception>
        public void DeleteSet(string key)
        {
            if (!_setsContext.IsSetExist(key))
                throw new SetNotFoundException("Set not found");
            _setsContext.DeleteSet(key);
        }

        #endregion METHODS
    }
}