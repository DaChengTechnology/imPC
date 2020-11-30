#pragma once
#include <emconversation.h>
#include "emsbsase.h"
#include "EMError.h"
#include "EMMessage.h"
namespace EaseMobLib {
	public enum class EMConversationType {
		CHAT,               //single chat
		GROUPCHAT,          //group chat
		CHATROOM,           //chatroom chat
		DISCUSSIONGROUP,    //discussion group chat
		HELPDESK,           //help desk chat
	};

	public enum class EMMessageSearchDirection {
		UP,					//Search older messages than reference
		DOWN,				//Search newer messages than reference
	};

	public ref class EMConversation : EMBase
	{
	public:
		EMConversation(easemob::EMConversationPtr&);

		EMConversation();
		/**
		* \brief Conversation destructor.
		*
		* @param  NA
		* @return NA
		*/
		virtual ~EMConversation();

		/**
		* \brief Get conversation id.
		*
		* Note: For a single chat conversation, it's remote peer's user name, for a group chat conversation, it's group id.
		* @param  NA
		* @return The conversation id.
		*/
		const String^ conversationId();

		/**
		* \brief Get conversation type.
		*
		* @param  NA
		* @return The conversation type.
		*/
		EMConversationType conversationType();

		/**
		* \brief Remove a message from DB and cache.
		*
		* Note: It's user's responsibility to confirm removed message belongs to the conversation.
		* @param  The message id
		* @return Return false if message isn't exist in DB.
		*/
		bool removeMessage(String^ msgId);

		/**
		* \brief Remove a message from DB and cache.
		*
		* Note: It's better to use this method to remove a message,
		and it's user's responsibility to confirm removed message belongs to the conversation.
		* @param  The message to remove
		* @return Return false if message isn't exist in DB.
		*/
		bool removeMessage(EMMessage^ msg);

		/**
		* \brief Insert a message to DB.
		*
		* Note: It's user's responsibility to confirm inserted message belongs to the conversation.
		* @param  The message to insert.
		* @return Return false if message can't insert to DB(e.g. has duplicate message with the same message id
		or user not login).
		*/
		bool insertMessage(EMMessage^ msg);

		/**
		* \brief Append a message to the last of conversation.
		*
		* Note: It's user's responsibility to confirm inserted message belongs to the conversation.
		* @param  The message to append.
		* @return Return false if message can't insert to DB(e.g. has duplicate message with the same message id
		or user not login).
		*/
		bool appendMessage(EMMessage^ msg);

		/**
		* \brief Update message's memory change to DB.
		*
		* Note: It's user's responsibility to confirm updated message belongs to the conversation, and user
		should NOT change a message's id.
		* @param  The message to remove
		* @return Return false if can't update message.
		*/
		bool updateMessage(EMMessage^ msg);

		/**
		* \brief Clear all messages belong to the the conversation(include DB and memory cache).
		*
		* @param  NA
		* @return Return false if can't clear the messages.
		*/
		bool clearAllMessages();

		/**
		* \brief Change message's read status.
		*
		* Note: It's user's responsibility to confirm changed message belongs to the conversation.
		* @param  The message to change.
		* @return Return false if message can't insert to DB(e.g. DB operation failed or read status doesn't need to change).
		*/
		bool markMessageAsRead(String^ msgId, bool isRead);

		/**
		* \brief Change all messages's read status.
		*
		* @param  NA
		* @return Return false if can't change read status.
		*/
		bool markAllMessagesAsRead();

		/**
		* \brief Get unread messages count of conversation.
		*
		* @param  NA
		* @return The unread messages count.
		*/
		int unreadMessagesCount();

		/**
		* \brief Get the total messages count of conversation.
		*
		* @param  NA
		* @return The total messages count.
		*/
		int messagesCount();

		/**
		* \brief Load a message(Will load message from DB if not exist in cache).
		*
		* @param  The message id
		* @return The loaded message.
		*/
		EMMessage^ loadMessage(String^ msgId);

		/**
		* \brief Get latest message of conversation.
		*
		* @param  NA
		* @return The latest message.
		*/
		EMMessage^ latestMessage();

		/**
		* \brief Get received latest message of conversation.
		*
		* @param  NA
		* @return The received latest message.
		*/
		EMMessage^ latestMessageFromOthers();

		/**
		* \brief Load specified number of messages from DB.
		*
		* Note: The return result will NOT include the reference message,
		and load message from the latest message if reference message id is empty.
		The result will be sorted by ASC.
		The trailing position resident last arrived message;
		* @param  The reference messages's id
		* @param  Message count to load
		* @return The loaded messages list.
		*/
		EMMessageList^ loadMoreMessages(String^ refMsgId, int count, EMMessageSearchDirection direction);

		/**
		* \brief Load specified number of messages before the timestamp from DB.
		*
		* Note: The result will be sorted by ASC.
		* @param  The reference timestamp
		* @param  Message count to load
		* @return The loaded messages list.
		*/
		EMMessageList^ loadMoreMessages(int64_t timeStamp, int count, EMMessageSearchDirection direction);

		/**
		* \brief Load specified number of messages before the timestamp and with the specified type from DB.
		*
		* Note: The result will be sorted by ASC.
		* @param  Message type to load
		* @param  The reference timestamp, milliseconds, will reference current time if timestamp is negative
		* @param  Message count to load, will load all messages meeet the conditions if count is negative
		* @return The loaded messages list.
		*/
		EMMessageList^ loadMoreMessages(EMMessageBodyType type, int64_t timeStamp, int count, const String^ from, EMMessageSearchDirection direction);

		/**
		* \brief Load specified number of messages before the timestamp and contains the specified keywords from DB.
		*
		* Note: The result will be sorted by ASC.
		* @param  Message contains keywords
		* @param  The reference timestamp, milliseconds, will reference current time if timestamp is negative
		* @param  Message count to load, will load all messages meeet the conditions if count is negative
		* @return The loaded messages list.
		*/
		EMMessageList^ loadMoreMessages(String^ keywords, int64_t timeStamp, int count, const String^ from, EMMessageSearchDirection direction);

		/**
		* \brief Load messages from DB.
		*
		* Note: To avoid occupy too much memory, user should limit the max messages count to load.
		The result will be sorted by ASC.
		The trailing position resident last arrived message;
		* @param  The start time timestamp
		* @param  The end time timestamp
		* @param  The max count of messages to load
		* @return The loaded messages list.
		*/
		EMMessageList^ loadMoreMessages(int64_t startTimeStamp, int64_t endTimeStamp, int maxCount);

		/**
		* \brief Get conversation extend attribute.
		*
		* @param  NA
		* @return The extend attribute.
		*/
		const String^ extField();

		/**
		* \brief Set conversation extend attribute.
		*
		* @param  The extend attribute.
		* @return Return false if set extend attribute failed.
		*/
		bool setExtField(String^ ext);
	private:
		easemob::EMConversationPtr& getImpl();
	};

	typedef cli::array<EMConversation^> EMConversationList;
}

