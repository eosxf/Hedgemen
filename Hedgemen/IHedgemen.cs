using System;

namespace Hgm;

public interface IHedgemen : IDisposable
{
	public void Run();
	public void Exit();
}