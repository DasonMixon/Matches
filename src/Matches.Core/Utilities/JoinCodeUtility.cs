using System;
using System.Collections.Generic;
using System.Linq;
using Matches.Core.Entities;

namespace Matches.Core.Utilities
{
    public static class JoinCodeUtility
    {
        private static readonly Random _rng = new Random();
        private const string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const int _size = 6;

        public static JoinCode Generate(IEnumerable<string> existingJoinCodes, int maxAttempts = 10)
        {
            var attempts = 0;

            while (attempts < maxAttempts)
            {
                var buffer = new char[_size];

                for (int i = 0; i < _size; i++)
                {
                    buffer[i] = _chars[_rng.Next(_chars.Length)];
                }

                var code = new string(buffer);

                if (!existingJoinCodes.Contains(code))
                    return new JoinCode { Id = code };

                attempts++;
            }

            throw new ApplicationException($"Unable to generate join code after {attempts} attempts");
        }
    }
}
