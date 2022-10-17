using PhlegmaticOne.Crypto.Symmetric.Feistel.Extensions;

namespace PhlegmaticOne.Crypto.Symmetric.Feistel.Keys;

public class ShiftRoundKeyGenerator : IRoundKeyGenerator
{
    public int TotalRounds { get; set; }

    public FeistelKey ChangeKey(FeistelKey feistelKey, int round)
    {
        if(TotalRounds % 2 == 0 && round == TotalRounds / 2)
        {
            return feistelKey;
        }

        if(round <= TotalRounds / 2)
        {
            return new(feistelKey.Key.ImmutableCycleLeftShift(1));
        }
        
        return new(feistelKey.Key.ImmutableCycleRightShift(1));
    }
}
