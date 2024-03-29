﻿using Aplication.Interfaces;
using Domain;

namespace Application.Interfaces;

public interface IUnitOfWork: IDisposable
{
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
    Task<int> Complete();
    
}