namespace PhlegmaticOne.Crypto.Symmetric.Feistel.Keys;

public interface IRoundKeyGenerator
{
    int TotalRounds { get; set; }
    FeistelKey ChangeKey(FeistelKey feistelKey, int round);
}
