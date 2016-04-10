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

    public void OnEditorMessage(EditorMessage em)
    {
        Debuger.Log("MessageInstance.EditorMessage" + em.name);
    }

    public void OnVBOBuffer(VBOBuffer buffer)
    {
        Debuger.Log("MessageInstance.OnVBOBuffer" + buffer.id);
        mObjModelRawMgr.Update(buffer);
    }

    public void OnVBOBufferSingle(VBOBufferSingle buffer)
    {
        Debuger.Log("MessageInstance.OnVBOBufferSingle" + buffer.id);
        mVBOBufferSingleMgr.Update(buffer);
    }

}
