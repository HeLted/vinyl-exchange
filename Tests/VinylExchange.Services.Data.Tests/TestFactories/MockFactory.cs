﻿namespace VinylExchange.Services.Data.Tests.TestFactories
{
    using Microsoft.AspNetCore.Identity;
    using Moq;

    internal static class MockFactory
    {
        public static Mock<UserManager<TUser>> MockUserManager<TUser>()
            where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();

            var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);

            mgr.Object.UserValidators.Add(new UserValidator<TUser>());

            mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

            return mgr;
        }
    }
}