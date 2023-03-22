namespace PRN211_ShoesStore.Models.DTO
{
	public class ChangeOrderStatusRequest
	{
		public int OrderId { get; set; }
		public int NewStatus { get; set; }
	}
}
