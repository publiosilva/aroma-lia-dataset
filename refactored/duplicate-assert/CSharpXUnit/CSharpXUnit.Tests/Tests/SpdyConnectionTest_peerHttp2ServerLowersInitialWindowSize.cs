public class SpdyConnectionTest {
    [Fact]
    public void PeerHttp2ServerLowersInitialWindowSize1() {
        peer.SetVariantAndClient(HTTP_20_DRAFT_09, false);

        Settings initial = new Settings();
        initial.Set(Settings.INITIAL_WINDOW_SIZE, PERSIST_VALUE, 1684);
        Settings shouldntImpactConnection = new Settings();
        shouldntImpactConnection.Set(Settings.INITIAL_WINDOW_SIZE, PERSIST_VALUE, 3368);

        peer.SendFrame().Settings(initial);
        peer.AcceptFrame(); // ACK
        peer.SendFrame().Settings(shouldntImpactConnection);
        peer.AcceptFrame(); // ACK 2
        peer.AcceptFrame(); // HEADERS
        peer.Play();

        SpdyConnection connection = connection(peer, HTTP_20_DRAFT_09);

        // verify the peer received the ACK
        MockSpdyPeer.InFrame ackFrame = peer.TakeFrame();
        Assert.Equal(TYPE_SETTINGS, ackFrame.type);
        Assert.Equal(0, ackFrame.streamId);
        Assert.True(ackFrame.ack);
        ackFrame = peer.TakeFrame();

        // This stream was created *after* the connection settings were adjusted.
        SpdyStream stream = connection.NewStream(headerEntries("a", "android"), false, true);

        Assert.Equal(3368, connection.peerSettings.GetInitialWindowSize(DEFAULT_INITIAL_WINDOW_SIZE));
        Assert.Equal(1684, connection.bytesLeftInWriteWindow); // initial wasn't affected.
        // New Stream has the most recent initial window size.
        Assert.Equal(3368, stream.bytesLeftInWriteWindow);
    }

    [Fact]
    public void PeerHttp2ServerLowersInitialWindowSize2() {
        peer.SetVariantAndClient(HTTP_20_DRAFT_09, false);

        Settings initial = new Settings();
        initial.Set(Settings.INITIAL_WINDOW_SIZE, PERSIST_VALUE, 1684);
        Settings shouldntImpactConnection = new Settings();
        shouldntImpactConnection.Set(Settings.INITIAL_WINDOW_SIZE, PERSIST_VALUE, 3368);

        peer.SendFrame().Settings(initial);
        peer.AcceptFrame(); // ACK
        peer.SendFrame().Settings(shouldntImpactConnection);
        peer.AcceptFrame(); // ACK 2
        peer.AcceptFrame(); // HEADERS
        peer.Play();

        SpdyConnection connection = connection(peer, HTTP_20_DRAFT_09);

        // verify the peer received the ACK
        MockSpdyPeer.InFrame ackFrame = peer.TakeFrame();
        ackFrame = peer.TakeFrame();
        Assert.Equal(TYPE_SETTINGS, ackFrame.type);
        Assert.Equal(0, ackFrame.streamId);
        Assert.True(ackFrame.ack);

        // This stream was created *after* the connection settings were adjusted.
        SpdyStream stream = connection.NewStream(headerEntries("a", "android"), false, true);

        Assert.Equal(3368, connection.peerSettings.GetInitialWindowSize(DEFAULT_INITIAL_WINDOW_SIZE));
        Assert.Equal(1684, connection.bytesLeftInWriteWindow); // initial wasn't affected.
        // New Stream has the most recent initial window size.
        Assert.Equal(3368, stream.bytesLeftInWriteWindow);
    }
}
