﻿using Entity;
using Repository;

namespace Service;

public class userService : IuserService
{
    private readonly IuserRepository userRepository;

    public userService(IuserRepository iuserRepository)
    {
        userRepository = iuserRepository;
    }

    public User addUser(User user)
    {
        int res = checkPassword(user.Password);
        if (res <= 2)
            return null;
        return userRepository.addUser(user);
    }

    public User getUserByEmailAndPassword(string userName, string password)
    {
        return userRepository.getUserByEmailAndPassword(userName, password);
    }

    public User updateUser(int id, User user)
    {
        int res = checkPassword(user.Password);
        if (res <= 2)
            return null;

        return userRepository.updateUser(id, user);
    }

    public int checkPassword(string pwd)
    {
        if (pwd != "")
        {
            var result = Zxcvbn.Core.EvaluatePassword(pwd);
            return result.Score;
        }
        return -1;

    }
}