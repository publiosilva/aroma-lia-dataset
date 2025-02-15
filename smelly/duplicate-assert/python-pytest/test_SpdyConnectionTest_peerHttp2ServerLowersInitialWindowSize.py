def test_peer_http2_server_lowers_initial_window_size():
    peer.set_variant_and_client(HTTP_20_DRAFT_09, False)

    initial = Settings()
    initial.set(Settings.INITIAL_WINDOW_SIZE, PERSIST_VALUE, 1684)
    shouldnt_impact_connection = Settings()
    shouldnt_impact_connection.set(Settings.INITIAL_WINDOW_SIZE, PERSIST_VALUE, 3368)

    peer.send_frame().settings(initial)
    peer.accept_frame()  # ACK
    peer.send_frame().settings(shouldnt_impact_connection)
    peer.accept_frame()  # ACK 2
    peer.accept_frame()  # HEADERS
    peer.play()

    connection = connection(peer, HTTP_20_DRAFT_09)

    ack_frame = peer.take_frame()
    assert ack_frame.type == TYPE_SETTINGS
    assert ack_frame.streamId == 0
    assert ack_frame.ack
    ack_frame = peer.take_frame()
    assert ack_frame.type == TYPE_SETTINGS
    assert ack_frame.streamId == 0
    assert ack_frame.ack

    stream = connection.new_stream(header_entries("a", "android"), False, True)

    assert connection.peerSettings.getInitialWindowSize(DEFAULT_INITIAL_WINDOW_SIZE) == 3368
    assert connection.bytesLeftInWriteWindow == 1684
    assert stream.bytesLeftInWriteWindow == 3368
