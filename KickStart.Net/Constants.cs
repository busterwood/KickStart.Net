﻿using System.Threading.Tasks;

namespace KickStart.Net
{
    public static class Constants
    {
        public static readonly Task<bool> True = Task.FromResult(true);
        public static readonly Task<bool> False = Task.FromResult(false);
    }
}
