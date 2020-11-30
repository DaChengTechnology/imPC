#pragma once
#include <emchatmanager_interface.h>
#include <message/emmessage.h>
#include "EMMessage.h"
#include "EMConversation.h"

namespace EaseMobLib {
	public ref class EMChatManagerListener
	{
	public:

		/**
		* \brief Callback user when receive a list of messages from remote peer.
		*
		* @param  The received messages.
		* @return NA
		*/
		delegate void D_onReceiveMessages(EMMessageList^ messages);

		/**
		* \brief Callback user when receive a list of command messages from remote peer.
		*
		* @param  The received command message.
		* @return NA
		*/
		delegate void D_onReceiveCmdMessages(EMMessageList^ messages);

		/**
		* \brief Callback user when send message successed or failed.
		*
		* Note: User will receive this callback only when not provide a message sending callback or callback returned false.
		* @param  The message to send.
		* @param  The occured error.
		* @return NA
		*/
		delegate void D_onMessageStatusChanged(const EMMessage^ message, const EMError^ error);

		/**
		* \brief Callback user when attachment download status changed.
		*
		* Note: User will receive this callback when thumbnail automatically download status changed or user download
		attachment and doesn't provide a callback.
		* @param  The message which's attachment is downloaded.
		* @param  The occured error.
		* @return NA
		*/
		delegate void D_onMessageAttachmentsStatusChanged(const EMMessage^ message, const EMError^ error);

		/**
		* \brief Callback user when receive read ack for messages.
		*
		* @param  The messages that receive read ack.
		* @return NA
		*/
		delegate void D_onReceiveHasReadAcks(EMMessageList^ messages);

		/**
		* \brief Callback user when receive delivery successed ack for messages.
		*
		* @param  The messages that receive delivery ack.
		* @return NA
		*/
		delegate void D_onReceiveHasDeliveredAcks(EMMessageList^ messages);

		/**
		* \brief Callback user when conversation list are changed.
		*
		* @param  The new conversation list.
		* @return NA
		*/
		delegate void D_onUpdateConversationList(EMConversationList^ conversations);
	public:
		D_onReceiveMessages^ onReceiveMessages = nullptr;
		D_onReceiveCmdMessages^ onReceiveCmdMessages = nullptr;
		D_onMessageStatusChanged^ onMessageStatusChanged = nullptr;
		D_onMessageAttachmentsStatusChanged^ onMessageAttachmentsStatusChanged = nullptr;
		D_onReceiveHasReadAcks^ onReceiveHasReadAcks = nullptr;
		D_onReceiveHasDeliveredAcks^ onReceiveHasDeliveredAcks = nullptr;
		D_onUpdateConversationList^ onUpdateConversationList = nullptr;
	};


	public ref class EMChatManager {
	public:
		EMChatManager(easemob::EMChatManagerInterface*);
		~EMChatManager();
		/**
		* \brief Send a message.
		*
		* Note: Will callback user by EMChatManagerListener if user doesn't provide a callback in the message or callback return false.
		* @param  The message to send.
		* @return NA
		*/
		void sendMessage(EMMessage^ msg);

		/**
		* \brief Send read ask for a message.
		*
		* @param  The message to send read ack.
		* @return NA
		*/
		void sendReadAckForMessage(EMMessage^);

		/**
		* \brief Resend a message.
		*
		* Note: Will callback user by EMChatManagerListener if user doesn't provide a callback in the message or callback return false.
		* @param  The message to resend.
		* @return NA
		*/
		void resendMessage(EMMessage^);

		/**
		* \brief Download thumbnail for image or video message.
		*
		* Note: Image or video message thumbnail is downloaded automatically,
		so user should NOT call this method except automatic download failed.
		And too, SDK will callback the user by EMChatManagerListener if user doesn't provide a callback in the message or callback return false.
		* @param  The message to download thumbnail.
		* @return NA
		*/
		void downloadMessageThumbnail(EMMessage^);

		/**
		* \brief Download attachment of a message.
		*
		* Note: User should call this method to download file, voice, image, video.
		And too, SDK will callback the user by EMChatManagerListener if user doesn't provide a callback or callback return false.
		* @param  The message to download attachment.
		* @return NA
		*/
		void downloadMessageAttachments(EMMessage^);

		/**
		* \brief Remove a conversation from DB and the memory.
		*
		* Note: Before remove a conversation, all conversations must have loaded from DB.
		* @param  The conversation id.
		* @param  The flag of whether remove the messages belongs to this conversation.
		* @return NA
		*/
		void removeConversation(String^ conversationId, bool isRemoveMessages);

		/**
		* \brief Get a conversation.
		*
		* Note: All conversations must have loaded from DB.
		* @param  The conversation id.
		* @param  The conversation type.
		* @param  The flag of whether created a conversation if it isn't exist.
		* @return The conversation
		*/
		EMConversation^ conversationWithType(String^ conversationId, EMConversationType type, bool createIfNotExist);

		/**
		* \brief Get all conversations from memory.
		*
		* Note: All conversations must have loaded from DB.
		* @param  NA
		* @return The conversation list
		*/
		EMConversationList^ getConversations();

		/**
		* \brief Get all conversations from DB.
		*
		* @param  NA
		* @return The conversation list
		*/
		EMConversationList^ loadAllConversationsFromDB();

		/**
		* \brief Add a listener to chat manager.
		*
		* @param  NA
		* @return NA
		*/
		void addListener(EMChatManagerListener^);

		/**
		* \brief Remove a listener.
		*
		* @param  NA
		* @return NA
		*/
		void removeListener(EMChatManagerListener^);

		/**
		* \brief Remove all the listeners.
		*
		* @param  NA
		* @return NA
		*/
		void clearListeners();

		/**
		* \brief Application can customize encrypt method through EMEncryptProvider
		*
		* Note: If EMConfigManager#KEY_USE_ENCRYPTION is true, but don't provider encryptprovider,
		* SDK will use default encrypt method.
		* @param  EMEncryptProvider Customized encrypt method provider.
		* @return NA
		*/
		//void setEncryptProvider(EMEncryptProviderInterface *provider);

		/**
		* \brief Get encrypt method being used.
		*
		* @param createIfNotExist If true, SDK will create a default encryptProvider, when there is no encryptProvider exists.
		* @return Encrypt method being used.
		*/
		//EMEncryptProviderInterface *getEncryptProvider(bool createIfNotExist = false);

		/**
		* \brief Insert messages.
		* @param  The messages to insert.
		* @return NA
		*/
		bool insertMessages(EMMessageList^ list);

		/**
		* \brief Get message by message Id.
		*
		* @param messageId
		* @return EMMessagePtr
		*/
		EMMessage^ getMessage(String^ messageId);

		/**
		* \brief Upload log to server.
		*/
		void uploadLog();
	private:
		void* listenerDelegate;
		easemob::EMChatManagerInterface* getImpl();
		easemob::EMChatManagerInterface* mImpl;
	};
}
