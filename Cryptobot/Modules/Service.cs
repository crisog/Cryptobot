using System;
using System.Threading;
using System.Threading.Tasks;
using Discord.Commands;

public class Service
{
    private Timer _timer;

    public Service(int start, int period, Action lambda)
    {
        Initialize(start, period, lambda);
    }
    public void Initialize(int i, int p, Action l)
    {
        _timer = new Timer(_ =>
        {
            l();
        },
        null,
        TimeSpan.FromSeconds(i),
        TimeSpan.FromHours(p));
    }
    // i wanna try something lol
}