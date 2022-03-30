using System;

namespace Hgm;

public interface IHedgemenBackend : IDisposable
{
	public void Run();
	public void Exit();
}