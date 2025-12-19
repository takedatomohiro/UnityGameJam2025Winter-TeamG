using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BackgroundData
{
    private static string _selectedBackgroundName;

    public static string selectedBackgroundName
    {
        get
        {
            if (string.IsNullOrEmpty(_selectedBackgroundName))
            {
                _selectedBackgroundName = "natu"; // ‰‰ñ‚¾‚¯İ’èI
            }
            return _selectedBackgroundName;
        }
        set
        {
            _selectedBackgroundName = value;
        }
    }

    public static readonly string[] availableBackgrounds = {
        "natu",
        "yoru",
        "yuki",
        "haru",
        "bg_winter",
        "bg_mountain"
    };
}
