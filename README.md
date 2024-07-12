# 角色建置
## 角色型別
ICharacter 為資料傳遞用底層資料結構，部分資訊不提供更改，Character 則作為角色循環使用建置型別
```
//提取用底層資料結構
public interface ICharacter
{
  public Mark                  Mark         { get; set; }
  public CharacterView         View         { get; }
  public CharacterController   Controller   { get; }
  public CalculateStats        Stats        { get; }
  public CharacterStateMachine StateMachine { get; }
}
//組成用基本角色型別
public class Character : ICharacter
{
  public Mark                  Mark         { get; set; }  
  public CharacterView         View         { get; set; }
  public CharacterController   Controller   { get; set; }
  public CalculateStats        Stats        { get; set; }
  public CharacterStateMachine StateMachine { get; set; }
}
```
### 建置過程
1. 發送角色建置事件(SpawnCharacter)
2. 生成角色，並發送各式角色部件索取事件
3. 根據事件回傳值完成角色組成
4. 將角色註冊至角色群組供外部調用
### 建置風險
1. ICharacter 以固定組成要素，難以擴充
2. 部件索取事件隨組成要素數量而累加，導致管理困難
3. 若在事件委派種另發事件，可能造成StackOverflow
### 未來改善
1.ICharacter 結構改善，增加組成彈性
```
public ICharacterElement {}

public ICharacterInstaller
{
  public void     Install(ICharacterElement element)
  public TElement Uninstall<TElement>() where TElement : ICharacterElement
}

public ICharacter
{
  public Mark Mark { get; set; }

  public TElement Get<TElement>()
}
```
2 ~ 3. 統一部件索取事件，取消回傳降低風險
```
//Origin，若在事件中發送事件，可能造成處理中事件序列重複調用，倒置StackOverflow
CharacterElementEvent(Action<TElement> Response)
//After，改以接受事件端處理，降低回傳風險
OnSpawnCharacter(ICharacterInstaller installer)
```
## 屬性互動
StatsManageModel：根據Mark從角色群組中提取角色進行角色屬性變動
StatsManagePresenter：接收屬性變更事件及進一步定義屬性變化行為
```
public class PlayerStatsPresenter : StatsMamangePresenter
{
  public class PlayerStatsPresenter(CharacterCollection collection, DomainEventService service) : base(collection, service)
  {
    StatIncreaseAction.Add("Health", HealthIncrease);
  }

  private void HealthIncrease(CalculateStat.Variable variable, ICharacter character)
  {
    //DoSomething
  }
}
```
### 互動衍伸
CharacterInteractModel 角色互動模組
```
public class CharacterInteractModel
{
  public CharacterCollection Collection { get; }

  public void Interact(Mark mark, Action<ICharacter> interact)
  {
    var character = Collection.GetGroup(mark.CharacterType)[mark];

    interacrt.Invoke(character);
  }
}
```
