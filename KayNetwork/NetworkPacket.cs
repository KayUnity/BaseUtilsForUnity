/********************************************************************************
** All rights reserved
** Auth： kay.yang
** E-mail: 1025115216@qq.com
** Date： 7/26/2017 3:57:46 PM
** Version:  v1.0.0
*********************************************************************************/

namespace NetworkWrapper
{
    public class NetworkPacket
    {
        public NetworkHeadFormat mHead;
        public byte[] mContent;
        public AbstractNetworkClient mClient;
        public NetworkPacket()
        {

        }
        public NetworkPacket(NetworkHeadFormat head, byte[] content, AbstractNetworkClient client)
        {
            mHead = head;
            mContent = content;
            mClient = client;
        }

        public byte[] GetBytes()
        {
            return mHead.Merge(mContent);
        }
    }

}

