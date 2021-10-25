namespace DDDNetCore.Domain.Users
{
public class Tag {
    private string name;

    public Tag(){}

    public Tag(string name){
        if(name != null){
            this.name = name;
        }
    }
}
}