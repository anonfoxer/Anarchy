﻿namespace Discord
{
    public class ChannelEventArgs
    {
        public Channel Channel { get; private set; }

        public ChannelEventArgs(Channel channel)
        {
            Channel = Channel;
        }


        public override string ToString()
        {
            return Channel.ToString();
        }
    }
}