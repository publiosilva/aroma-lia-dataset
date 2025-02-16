import pytest

class TestSpdyConnection:
    def test_peer_http2_server_lowers_initial_window_size_1(self):
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

        # verify the peer received the ACK
        ack_frame = peer.take_frame()
        assert ack_frame.type == TYPE_SETTINGS
        assert ack_frame.stream_id == 0
        assert ack_frame.ack
        ack_frame = peer.take_frame()

        stream = connection.new_stream(header_entries("a", "android"), False, True)

        assert connection.peer_settings.get_initial_window_size(DEFAULT_INITIAL_WINDOW_SIZE) == 3368
        assert connection.bytes_left_in_write_window == 1684  # initial wasn't affected.
        assert stream.bytes_left_in_write_window == 3368  # New Stream has the most recent initial window size.

    def test_peer_http2_server_lowers_initial_window_size_2(self):
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

        # verify the peer received the ACK
        ack_frame = peer.take_frame()
        ack_frame = peer.take_frame()
        assert ack_frame.type == TYPE_SETTINGS
        assert ack_frame.stream_id == 0
        assert ack_frame.ack

        stream = connection.new_stream(header_entries("a", "android"), False, True)

        assert connection.peer_settings.get_initial_window_size(DEFAULT_INITIAL_WINDOW_SIZE) == 3368
        assert connection.bytes_left_in_write_window == 1684  # initial wasn't affected.
        assert stream.bytes_left_in_write_window == 3368  # New Stream has the most recent initial window size.
