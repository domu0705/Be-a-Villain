// -------------------------------------------------------------------------------------------------
// quest �ϳ��� ������ ���� ( ����Ʈ �̸�, ����Ʈ ������ ���� �ʿ��� npc�� id(��ȣ))
// -------------------------------------------------------------------------------------------------
public class QuestData
{
    public string questName;
    public int[] npcId;

    public QuestData(string name, int[] npc)
    {
        questName = name;
        npcId = npc;
    }
}
