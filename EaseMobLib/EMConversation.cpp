#include "pch.h"
#include "EMConversation.h"
#include "EMUtils.h"

namespace EaseMobLib {
	EMConversation::EMConversation(easemob::EMConversationPtr& _ptr) {
		setNativeHandler<easemob::EMConversationPtr>(&_ptr);
	}

	EMConversation::EMConversation() {
	}

	/**
	* \brief Conversation destructor.
	*
	* @param  NA
	* @return NA
	*/
	EMConversation::~EMConversation() {
		nativeFinalize<easemob::EMConversationPtr>();
	}

	/**
	* \brief Get conversation id.
	*
	* Note: For a single chat conversation, it's remote peer's user name, for a group chat conversation, it's group id.
	* @param  NA
	* @return The conversation id.
	*/
	const String^ EMConversation::conversationId() {
		return getCSString(getImpl()->conversationId());
	}

	/**
	* \brief Get conversation type.
	*
	* @param  NA
	* @return The conversation type.
	*/
	EMConversationType EMConversation::conversationType() {
		return (EMConversationType)getImpl()->conversationType();
	}

	/**
	* \brief Remove a message from DB and cache.
	*
	* Note: It's user's responsibility to confirm removed message belongs to the conversation.
	* @param  The message id
	* @return Return false if message isn't exist in DB.
	*/
	bool EMConversation::removeMessage(String^ msgId) {
		return getImpl()->removeMessage(extractCSString(msgId));
	}

	/**
	* \brief Remove a message from DB and cache.
	*
	* Note: It's better to use this method to remove a message,
	and it's user's responsibility to confirm removed message belongs to the conversation.
	* @param  The message to remove
	* @return Return false if message isn't exist in DB.
	*/
	bool EMConversation::removeMessage(EMMessage^ csMsg) {
		easemob::EMMessagePtr& msg = csMsg->getNative<easemob::EMMessagePtr>();
		return getImpl()->removeMessage(msg);
	}

	/**
	* \brief Insert a message to DB.
	*
	* Note: It's user's responsibility to confirm inserted message belongs to the conversation.
	* @param  The message to insert.
	* @return Return false if message can't insert to DB(e.g. has duplicate message with the same message id
	or user not login).
	*/
	bool EMConversation::insertMessage(EMMessage^ msg) {
		return getImpl()->insertMessage(msg->getNative<easemob::EMMessagePtr>());
	}

	bool EMConversation::appendMessage(EMMessage^ msg) {
		return getImpl()->appendMessage(msg->getNative<easemob::EMMessagePtr>());
	}

	/**
	* \brief Update message's memory change to DB.
	*
	* Note: It's user's responsibility to confirm updated message belongs to the conversation, and user
	should NOT change a message's id.
	* @param  The message to remove
	* @return Return false if can't update message.
	*/
	bool EMConversation::updateMessage(EMMessage^ msg) {
		return getImpl()->updateMessage(msg->getNative<easemob::EMMessagePtr>());
	}

	/**
	* \brief Clear all messages belong to the the conversation(include DB and memory cache).
	*
	* @param  NA
	* @return Return false if can't clear the messages.
	*/
	bool EMConversation::clearAllMessages() {
		return getImpl()->clearAllMessages();
	}

	/**
	* \brief Change message's read status.
	*
	* Note: It's user's responsibility to confirm changed message belongs to the conversation.
	* @param  The message to change.
	* @return Return false if message can't insert to DB(e.g. DB operation failed or read status doesn't need to change).
	*/
	bool EMConversation::markMessageAsRead(String^ msgId, bool isRead) {
		return getImpl()->markMessageAsRead(extractCSString(msgId), isRead);
	}

	/**
	* \brief Change all messages's read status.
	*
	* @param  NA
	* @return Return false if can't change read status.
	*/
	bool EMConversation::markAllMessagesAsRead() {
		return getImpl()->markAllMessagesAsRead(true);
	}

	/**
	* \brief Get unread messages count of conversation.
	*
	* @param  NA
	* @return The unread messages count.
	*/
	int EMConversation::unreadMessagesCount() {
		return getImpl()->unreadMessagesCount();
	}

	/**
	* \brief Get the total messages count of conversation.
	*
	* @param  NA
	* @return The total messages count.
	*/
	int EMConversation::messagesCount() {
		return getImpl()->messagesCount();
	}

	/**
	* \brief Load a message(Will load message from DB if not exist in cache).
	*
	* @param  The message id
	* @return The loaded message.
	*/
	EMMessage^ EMConversation::loadMessage(String^ msgId) {
		easemob::EMMessagePtr msg = getImpl()->loadMessage(extractCSString(msgId));
		if (msg == nullptr) {
			return nullptr;
		}
		easemob::EMMessagePtr* ptr = new easemob::EMMessagePtr(msg);
		return getCSMessage(ptr);
	}

	/**
	* \brief Get latest message of conversation.
	*
	* @param  NA
	* @return The latest message.
	*/
	EMMessage^ EMConversation::latestMessage() {
		easemob::EMMessagePtr msg = getImpl()->latestMessage();
		if (msg == nullptr) {
			return nullptr;
		}
		easemob::EMMessagePtr* ptr = new easemob::EMMessagePtr(msg);
		return getCSMessage(ptr);
	}

	/**
	* \brief Get received latest message of conversation.
	*
	* @param  NA
	* @return The received latest message.
	*/
	EMMessage^ EMConversation::latestMessageFromOthers() {
		easemob::EMMessagePtr msg = getImpl()->latestMessageFromOthers();
		easemob::EMMessagePtr* ptr = new easemob::EMMessagePtr(msg);
		return getCSMessage(ptr);
	}

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
	EMMessageList^ EMConversation::loadMoreMessages(String^ refMsgId, int count, EMMessageSearchDirection direction) {
		easemob::EMMessageList list = getImpl()->loadMoreMessages(extractCSString(refMsgId), count, (easemob::EMConversation::EMMessageSearchDirection)direction);
		EMMessageList^ csList = gcnew EMMessageList(list.size());
		int i = 0;
		for (easemob::EMMessagePtr& msg : list) {
			easemob::EMMessagePtr* ptr = new easemob::EMMessagePtr(msg);
			EMMessage^ csMsg = getCSMessage(ptr);
			csList[i] = csMsg;
			i++;
		}
		return csList;
	}

	/**
	* \brief Load specified number of messages before the timestamp from DB.
	*
	* Note: The result will be sorted by ASC.
	* @param  The reference timestamp
	* @param  Message count to load
	* @return The loaded messages list.
	*/
	EMMessageList^ EMConversation::loadMoreMessages(int64_t timeStamp, int count, EMMessageSearchDirection direction) {
		easemob::EMMessageList list = getImpl()->loadMoreMessages(timeStamp, count, (easemob::EMConversation::EMMessageSearchDirection)direction);
		EMMessageList^ csList = gcnew EMMessageList(list.size());
		int i = 0;
		for (easemob::EMMessagePtr& msg : list) {
			easemob::EMMessagePtr* ptr = new easemob::EMMessagePtr(msg);
			EMMessage^ csMsg = getCSMessage(ptr);
			csList[i] = csMsg;
			i++;
		}
		return csList;
	}

	/**
	* \brief Load specified number of messages before the timestamp and with the specified type from DB.
	*
	* Note: The result will be sorted by ASC.
	* @param  Message type to load
	* @param  The reference timestamp, milliseconds, will reference current time if timestamp is negative
	* @param  Message count to load, will load all messages meeet the conditions if count is negative
	* @return The loaded messages list.
	*/
	EMMessageList^ EMConversation::loadMoreMessages(EMMessageBodyType type, int64_t timeStamp, int count, const String^ from, EMMessageSearchDirection direction) {
		easemob::EMMessageList list = getImpl()->loadMoreMessages((easemob::EMMessageBody::EMMessageBodyType)type, timeStamp, count, extractCSString(from), (easemob::EMConversation::EMMessageSearchDirection)direction);
		EMMessageList^ csList = gcnew EMMessageList(list.size());
		int i = 0;
		for (easemob::EMMessagePtr& msg : list) {
			easemob::EMMessagePtr* ptr = new easemob::EMMessagePtr(msg);
			EMMessage^ csMsg = getCSMessage(ptr);
			csList[i] = csMsg;
			i++;
		}
		return csList;
	}

	/**
	* \brief Load specified number of messages before the timestamp and contains the specified keywords from DB.
	*
	* Note: The result will be sorted by ASC.
	* @param  Message contains keywords
	* @param  The reference timestamp, milliseconds, will reference current time if timestamp is negative
	* @param  Message count to load, will load all messages meeet the conditions if count is negative
	* @return The loaded messages list.
	*/
	EMMessageList^ EMConversation::loadMoreMessages(String^ keywords, int64_t timeStamp, int count, const String^ from, EMMessageSearchDirection direction) {
		easemob::EMMessageList list = getImpl()->loadMoreMessages(extractCSString(keywords), timeStamp, count, extractCSString(from), (easemob::EMConversation::EMMessageSearchDirection)direction);
		EMMessageList^ csList = gcnew EMMessageList(list.size());
		int i = 0;
		for (easemob::EMMessagePtr& msg : list) {
			easemob::EMMessagePtr* ptr = new easemob::EMMessagePtr(msg);
			EMMessage^ csMsg = getCSMessage(ptr);
			csList[i] = csMsg;
			i++;
		}
		return csList;
	}

	EMMessageList^ EMConversation::loadMoreMessages(int64_t startTimeStamp, int64_t endTimeStamp, int maxCount) {
		easemob::EMMessageList list = getImpl()->loadMoreMessages(startTimeStamp, endTimeStamp, maxCount);
		EMMessageList^ csList = gcnew EMMessageList(list.size());
		int i = 0;
		for (easemob::EMMessagePtr& msg : list) {
			easemob::EMMessagePtr* ptr = new easemob::EMMessagePtr(msg);
			EMMessage^ csMsg = getCSMessage(ptr);
			csList[i] = csMsg;
			i++;
		}
		return csList;
	}

	/**
	* \brief Get conversation extend attribute.
	*
	* @param  NA
	* @return The extend attribute.
	*/
	const String^ EMConversation::extField() {
		return getCSString(getImpl()->extField());
	}

	/**
	* \brief Set conversation extend attribute.
	*
	* @param  The extend attribute.
	* @return Return false if set extend attribute failed.
	*/
	bool EMConversation::setExtField(String^ ext) {
		return getImpl()->setExtField(extractCSString(ext));
	}

	easemob::EMConversationPtr& EMConversation::getImpl() {
		return getNative<easemob::EMConversationPtr>();
	}
}