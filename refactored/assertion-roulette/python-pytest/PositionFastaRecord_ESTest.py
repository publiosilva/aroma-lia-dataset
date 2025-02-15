import pytest
from unittest.mock import Mock

@pytest.mark.timeout(4000)
def test00():
    position_sequence0 = Mock()
    position_sequence0.__str__.return_value = "pb995}/N"
    
    position_fasta_record0 = PositionFastaRecord("", "", position_sequence0)
    position_sequence1 = position_fasta_record0.get_sequence()
    position_fasta_record1 = PositionFastaRecord("PositionSequenceFastaRecord [identifier=, comments=, positions=pb995}/N]", "ml9H%7|,g{C$R-jG:Hx", position_sequence1)
    
    position_fasta_record1.__hash__()
    assert position_fasta_record1.get_comment() == "ml9H%7|,g{C$R-jG:Hx", "Explanation message"
    assert position_fasta_record1.get_id() == "PositionSequenceFastaRecord [identifier=, comments=, positions=pb995}/N]", "Explanation message"
