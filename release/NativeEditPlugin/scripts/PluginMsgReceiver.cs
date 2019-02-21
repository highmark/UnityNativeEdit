using UnityEngine;

public abstract class PluginMsgReceiver : MonoBehaviour
{
	private	int?		nReceiverId = null;

	protected virtual void Start()
	{
		nReceiverId = PluginMsgHandler.getInst().RegisterAndGetReceiverId(this);
	}

	protected virtual void OnDestroy()
	{
        if (nReceiverId != null) {
		    PluginMsgHandler.getInst().RemoveReceiver((int)nReceiverId);
        }
	}

	protected JsonObject SendPluginMsg(JsonObject jsonMsg)
	{
        if (nReceiverId != null) {
		    return PluginMsgHandler.getInst().SendMsgToPlugin((int)nReceiverId, jsonMsg);
        } else {
            throw new System.InvalidOperationException("Attempted to send a NativeEdit plugin msg for a PluginMsgReceiver that has not been initialized.");
        }
	}

	public abstract void OnPluginMsgDirect(JsonObject jsonMsg);  
}