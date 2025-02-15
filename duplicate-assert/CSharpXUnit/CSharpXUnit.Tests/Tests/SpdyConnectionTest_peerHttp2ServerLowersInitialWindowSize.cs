public class SpdyConnectionTest {
  [Fact]
  public void PeerHttp2ServerLowersInitialWindowSize() {
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

    SpdyConnection connection = Connection(peer, HTTP_20_DRAFT_09);

    // verify the peer received the ACK
    MockSpdyPeer.InFrame ackFrame = peer.TakeFrame();
    Assert.Equal(TYPE_SETTINGS, ackFrame.Type);
    Assert.Equal(0, ackFrame.StreamId);
    Assert.True(ackFrame.Ack);
    ackFrame = peer.TakeFrame();
    Assert.Equal(TYPE_SETTINGS, ackFrame.Type);
    Assert.Equal(0, ackFrame.StreamId);
    Assert.True(ackFrame.Ack);

    // This stream was created *after* the connection settings were adjusted.
    SpdyStream stream = connection.NewStream(HeaderEntries("a", "android"), false, true);

    Assert.Equal(3368, connection.PeerSettings.GetInitialWindowSize(DEFAULT_INITIAL_WINDOW_SIZE));
    Assert.Equal(1684, connection.BytesLeftInWriteWindow); // initial wasn't affected.
    // New Stream has the most recent initial window size.
    Assert.Equal(3368, stream.BytesLeftInWriteWindow);
  }
}
