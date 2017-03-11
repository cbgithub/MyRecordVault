using MyRecordVault.Models;
using System;

namespace MyRecordVault.Helpers
{
    public class GeneratePassword
    {


        public string _buildPassword;
        public string _password;

        public GeneratePassword(Password model)
        {



            if (model.CaseSensitive)
            {
                var _upperLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                _buildPassword += _upperLetters;
            }

            if (!model.CaseSensitive)
            {
                var _lowerCase = "abcdefghijklmnopqrstuvwxyz";
                _buildPassword += _lowerCase;
            }

            if (model.Digits)
            {
                var _digits = "0123456789";
                _buildPassword += _digits;
            }
            if (model.SpecialCharacters)
            {
                var _specialCharacters = "!@$?_-";
                _buildPassword += _specialCharacters;
            }


            var stringChars = new char[model.Length];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = _buildPassword[random.Next(_buildPassword.Length)];
            }

            _password = new String(stringChars);

        }

    }
}
