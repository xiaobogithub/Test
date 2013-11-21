using System.Windows;

namespace iMeta.Windows
{
	public interface IUIElementContainer
	{
		bool Contains(UIElement element);

		bool IsSingleChildContainer
		{ 
			get;
		}
		
		bool Remove(UIElement element);
		void Add(UIElement element);
	}
}
