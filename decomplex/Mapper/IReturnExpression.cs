﻿using System;

namespace decomplex.Mapper
{
    public interface IReturnExpression<TKey, TValue>
    {
        IMapper<TKey, TValue> Return(TValue value);
        IMapper<TKey, TValue> Return(Func<TValue> valueFactory);
    }
}