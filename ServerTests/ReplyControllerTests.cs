namespace ServerTests;

public class ReplyControllerTests
{
    //GetReplies_InvalidPostId_ReturnOkAndEmptyList
    //GetReplies_ValidPostId_ReturnOkAndPostList
    
    //CreateReply_ValidPost_ReturnCreatedAndPost
    //CreateReply_InvalidPostId_ReturnBadRequest
    
    //UpdateReply_ValidReplyUserNotOwner_ReturnUnauthorized
    //UpdateReply_NotValidReplyUserNotOwner_ReturnNoContent
    //UpdateReply_NotValidReplyUserIsOwner_ReturnNoContent
    //UpdateReply_ValidReplyUserIsOwner_ReturnOkayAndReply
    //UpdateReply_ValidReplyUserNotOwnerButAdmin_ReturnOkayAndReply
    
    //DeletePost_ValidReplyUserNotOwner_ReturnUnauthorized
    //DeletePost_NotValidReplyUserNotOwner_ReturnNoContent
    //DeletePost_NotValidReplyUserIsOwner_ReturnNoContent
    //DeletePost_ValidReplyUserIsOwner_ReturnOkayAndReply
    //DeletePost_ValidReplyUserNotOwnerButAdmin_ReturnOkayAndReply
}