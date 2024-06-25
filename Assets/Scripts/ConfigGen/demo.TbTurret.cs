
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Luban;


namespace cfg.demo
{
public partial class TbTurret
{
    private readonly System.Collections.Generic.Dictionary<int, Turret> _dataMap;
    private readonly System.Collections.Generic.List<Turret> _dataList;
    
    public TbTurret(ByteBuf _buf)
    {
        _dataMap = new System.Collections.Generic.Dictionary<int, Turret>();
        _dataList = new System.Collections.Generic.List<Turret>();
        
        for(int n = _buf.ReadSize() ; n > 0 ; --n)
        {
            Turret _v;
            _v = Turret.DeserializeTurret(_buf);
            _dataList.Add(_v);
            _dataMap.Add(_v.Id, _v);
        }
    }

    public System.Collections.Generic.Dictionary<int, Turret> DataMap => _dataMap;
    public System.Collections.Generic.List<Turret> DataList => _dataList;

    public Turret GetOrDefault(int key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public Turret Get(int key) => _dataMap[key];
    public Turret this[int key] => _dataMap[key];

    public void ResolveRef(Tables tables)
    {
        foreach(var _v in _dataList)
        {
            _v.ResolveRef(tables);
        }
    }

}

}
