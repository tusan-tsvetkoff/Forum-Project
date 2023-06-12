# Data Models

## Comment

```csharp
class Comment
{
    Menu Create(Guid postId);
    void Delete(Guid id);
    void EditComment(Menu content);    
}
```

```json
{
    "id": "0000-000000000-000-0000-000000000",
    "content": "A secret mystery comment.",
    "createdDateTime": "2023-01-01T00:00:00:0000000Z",
    "editedDateTime": "2023-01-01T00:00:00:0000000Z",
    "userId": "0000-000000-000-00000-00000000",
    "postId": "000-000-000000-00000-000000000",
}
```