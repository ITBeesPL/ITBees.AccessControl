namespace ITBees.AccessControl.Interfaces.ViewModels;

public class DeleteAccessCardResultVm
{
    public List<Guid> DeletedCardsSuccess { get; set; }
    public List<Guid> DeletedCardsFail{ get; set; }
}