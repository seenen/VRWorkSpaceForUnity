using LibVRGeometry;
using LibVRGeometry.Message;

public class MessageInstance : IMessage
{
    ObjModelRawMgr mObjModelRawMgr;
    VBOBufferSingleMgr mVBOBufferSingleMgr;

    public MessageInstance()
    {
        mObjModelRawMgr = new ObjModelRawMgr();
        mVBOBufferSingleMgr = new VBOBufferSingleMgr();
    }

    /// <summary>
    /// 编辑器消息
    /// </summary>
    /// <param name="em"></param>
    public void OnEditorMessage(EditorMessage em)
    {
        Debuger.Log("MessageInstance.EditorMessage" + em.name);
    }

    /// <summary>
    /// 2组以上对象数据格式
    /// </summary>
    /// <param name="buffer"></param>
    public void OnVBOBuffer(VBOBuffer buffer)
    {
        Debuger.Log("MessageInstance.OnVBOBuffer" + buffer.id);
        mObjModelRawMgr.Update(buffer);
    }

    /// <summary>
    /// 单对象数据格式
    /// </summary>
    /// <param name="buffer"></param>
    public void OnVBOBufferSingle(VBOBufferSingle buffer)
    {
        Debuger.Log("MessageInstance.OnVBOBufferSingle" + buffer.id);
        switch(buffer.vboType)
        {
            case VBOType.Null:
                break;
            case VBOType.DOT_OBJ:
                mVBOBufferSingleMgr.Update(buffer);
                break;
            case VBOType.U3D_MESH:
                break;
        }
    }

}
