public final class SpdyConnectionTest {
  @Test public void peerHttp2ServerLowersInitialWindowSize1() throws Exception {
    peer.setVariantAndClient(HTTP_20_DRAFT_09, false);

    Settings initial = new Settings();
    initial.set(Settings.INITIAL_WINDOW_SIZE, PERSIST_VALUE, 1684);
    Settings shouldntImpactConnection = new Settings();
    shouldntImpactConnection.set(Settings.INITIAL_WINDOW_SIZE, PERSIST_VALUE, 3368);

    peer.sendFrame().settings(initial);
    peer.acceptFrame(); // ACK
    peer.sendFrame().settings(shouldntImpactConnection);
    peer.acceptFrame(); // ACK 2
    peer.acceptFrame(); // HEADERS
    peer.play();

    SpdyConnection connection = connection(peer, HTTP_20_DRAFT_09);

    // verify the peer received the ACK
    MockSpdyPeer.InFrame ackFrame = peer.takeFrame();
    assertEquals(TYPE_SETTINGS, ackFrame.type);
    assertEquals(0, ackFrame.streamId);
    assertTrue(ackFrame.ack);
    ackFrame = peer.takeFrame();

    // This stream was created *after* the connection settings were adjusted.
    SpdyStream stream = connection.newStream(headerEntries("a", "android"), false, true);

    assertEquals(3368, connection.peerSettings.getInitialWindowSize(DEFAULT_INITIAL_WINDOW_SIZE));
    assertEquals(1684, connection.bytesLeftInWriteWindow); // initial wasn't affected.
    // New Stream is has the most recent initial window size.
    assertEquals(3368, stream.bytesLeftInWriteWindow);
  }

  @Test public void peerHttp2ServerLowersInitialWindowSize2() throws Exception {
    peer.setVariantAndClient(HTTP_20_DRAFT_09, false);

    Settings initial = new Settings();
    initial.set(Settings.INITIAL_WINDOW_SIZE, PERSIST_VALUE, 1684);
    Settings shouldntImpactConnection = new Settings();
    shouldntImpactConnection.set(Settings.INITIAL_WINDOW_SIZE, PERSIST_VALUE, 3368);

    peer.sendFrame().settings(initial);
    peer.acceptFrame(); // ACK
    peer.sendFrame().settings(shouldntImpactConnection);
    peer.acceptFrame(); // ACK 2
    peer.acceptFrame(); // HEADERS
    peer.play();

    SpdyConnection connection = connection(peer, HTTP_20_DRAFT_09);

    // verify the peer received the ACK
    MockSpdyPeer.InFrame ackFrame = peer.takeFrame();
    ackFrame = peer.takeFrame();
    assertEquals(TYPE_SETTINGS, ackFrame.type);
    assertEquals(0, ackFrame.streamId);
    assertTrue(ackFrame.ack);

    // This stream was created *after* the connection settings were adjusted.
    SpdyStream stream = connection.newStream(headerEntries("a", "android"), false, true);

    assertEquals(3368, connection.peerSettings.getInitialWindowSize(DEFAULT_INITIAL_WINDOW_SIZE));
    assertEquals(1684, connection.bytesLeftInWriteWindow); // initial wasn't affected.
    // New Stream is has the most recent initial window size.
    assertEquals(3368, stream.bytesLeftInWriteWindow);
  }
}
