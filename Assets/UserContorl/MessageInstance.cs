using LibVRGeometry;
using LibVRGeometry.Message;

public class MessageInstance : IMessage
{
    ObjModelRawMgr mObjModelRawMgr;

    public MessageInstance()
    {
        mObjModelRawMgr = new ObjModelRawMgr();
    }

    public void EditorMessage(EditorMessage em)
    {
        Debuger.Log("MessageInstance.EditorMessage" + em.name);
    }

    public void OnVBOBuffer(VBOBuffer buffer)
    {
        Debuger.Log("MessageInstance.OnVBOBuffer" + buffer.id);

        mObjModelRawMgr.Update(buffer);
    }

}
