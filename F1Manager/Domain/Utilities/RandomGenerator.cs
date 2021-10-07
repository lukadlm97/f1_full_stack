using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Utilities
{
    public class RandomGenerator
    {
        public int GenerateVerificationCode()
        {
            Random rng = new Random();

            return rng.Next();
        }
    }
}
