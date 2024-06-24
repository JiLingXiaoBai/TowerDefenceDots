using System.IO;
using cfg;
using Luban;
using UnityEngine;

public class TDRoot
{
    public static readonly Tables Tables;

    static TDRoot()
    {
        Tables = new Tables(LoadByteBuf);
    }

    private static ByteBuf LoadByteBuf(string file)
    {
        return new ByteBuf(File.ReadAllBytes($"{Application.dataPath}/../Config/output/{file}.bytes"));
    }
}