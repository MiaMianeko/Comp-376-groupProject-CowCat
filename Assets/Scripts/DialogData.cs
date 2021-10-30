using System;
using System.Collections.Generic;

public class DialogData
{
    public List<JsonDialogData> data;
}

[Serializable]
public class JsonDialogData
{
    public string speaker;
    public string content;
    public float duration;
}