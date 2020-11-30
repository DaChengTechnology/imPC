#pragma once
#include "include/message/emmessage.h"
#include "emsbsase.h"
#include "EMMessageBody.h"
#include "EMCallBack.h"
#include "EMError.h"

using namespace System;
using namespace System::Collections::Generic;
using namespace System::Runtime::InteropServices;

namespace EaseMobLib {
	/**
	*  Message chat type.
	*/
	public enum class EMChatType
	{
		SINGLE,     //Single chat
		GROUP,      //Group chat
		CHATROOM    //Chatroom chat
	};

	/**
	*  Message status.
	*/
	public enum class EMMessageStatus
	{
		NEW,            //New message
		DELIVERING,     //Message is delivering
		SUCCESS,        //Message delivering successed
		FAIL            //Message delivering failed
	};

	public enum class EMMessageDirection
	{
		SEND,
		RECEIVE
	};

	public ref class EMMessage:EMBase
	{
	public:
		/**
		* \brief Class destructor.
		*
		* @param  NA
		* @return NA
		*/
		virtual ~EMMessage();

		/**
		* \brief Get message id.
		*
		* @param  NA
		* @return The message id.
		*/
		const String^ msgId();

		/**
		* \brief Set message id.
		*
		* Note: User should never change a message's id if you don't want to save as a new message.
		* @param  The new message id.
		* @return NA.
		*/
		void setMsgId(const String^);

		/**
		* \brief Get message sender.
		*
		* @param  NA
		* @return The message sender.
		*/
		const String^ from();

		/**
		* \brief Set message sender.
		*
		* @param  The new message sender.
		* @return NA.
		*/
		void setFrom(const String^);

		/**
		* \brief Get message receiver.
		*
		* @param  NA
		* @return The message receiver.
		*/
		const String^ to();

		/**
		* \brief Set message receiver.
		*
		* @param  The new message receiver.
		* @return NA.
		*/
		void setTo(const String^);

		/**
		* \brief Get conversation id.
		*
		* @param  NA
		* @return The conversation id.
		*/
		const String^ conversationId();

		/**
		* \brief Set message's conversation id.
		*
		* Note: User should NOT change message's conversation id after receive or send a message.
		* @param  The new conversation id.
		* @return NA.
		*/
		void setConversationId(const String^);

		/**
		* \brief Get message status.
		*
		* @param  NA
		* @return The message status.
		*/
		EMMessageStatus status();

		/**
		* \brief Set message's status.
		*
		* Note: User should NOT change message's status directly.
		* @param  The new message status.
		* @return NA.
		*/
		void setStatus(EMMessageStatus);

		/**
		* \brief Get message chat type.
		*
		* @param  NA
		* @return The message chat type.
		*/
		EMChatType chatType();

		/**
		* \brief Set message's chat type.
		*
		* Note: User should NOT change message's chat type after receive or send a message.
		* @param  The new conversation id.
		* @return NA.
		*/
		void setChatType(EMChatType);

		/**
		* \brief Get message direction.
		*
		* @param  NA
		* @return The message direction.
		*/
		EMMessageDirection msgDirection();

		/**
		* \brief Set message's direction.
		*
		* Note: User should NOT change message's message direction after receive or send a message.
		* @param  NA
		* @return NA.
		*/
		void setMsgDirection(EMMessageDirection);

		/**
		* \brief Get message if has read status.
		*
		* @param  NA
		* @return The message read status.
		*/
		bool isRead();

		/**
		* \brief Set message's read status.
		*
		* Note: User should NOT change message's read status directly.
		* @param  The new message read status.
		* @return NA.
		*/
		void setIsRead(bool isRead);

		/**
		* \brief Get message if has listened status.
		*
		* @param  NA
		* @return The message listened status.
		*/
		bool isListened();

		/**
		* \brief Set message's listened status.
		*
		* Note: User should NOT change message's listened status directly.
		* @param  The new message listened status.
		* @return NA.
		*/
		void setIsListened(bool isListened);

		/**
		* \brief Get message read ack status.
		*
		* Note: For receiver, it indicates whether has sent read ack, and for sender, it indicates whether has received read ack.
		* @param  NA
		* @return The message read ack status.
		*/
		bool isReadAcked();

		/**
		* \brief Set message's read ack status.
		*
		* Note: User should NOT change message's read ack status directly.
		* @param  The new message read ack status.
		* @return NA.
		*/
		void setIsReadAcked(bool);

		/**
		* \brief Get message delivering status.
		*
		* Note: For receiver, it indicates whether has sent delivering successed ack, and for sender, it indicates whether has received delivering successed ack.
		* @param  NA
		* @return The message delivering status.
		*/
		bool isDeliverAcked();

		/**
		* \brief Set message's delivery ack status.
		*
		* Note: User should NOT change message's delivery ack status directly.
		* @param  The new message delivery ack status.
		* @return NA.
		*/
		void setIsDeliverAcked(bool);

		/**
		* \brief Get message timestamp.
		*
		* @param  NA
		* @return The message timestamp.
		*/
		int64_t timestamp();

		/**
		* \brief Set message's timestamp.
		*
		* Note: User should NOT change message's timestamp.
		* @param  The new message timestamp.
		* @return NA.
		*/
		void setTimestamp(int64_t);

		/**
		* \brief Get message body list.
		*
		* @param  NA
		* @return The message body list.
		*/
		cli::array<EMMessageBody^>^ bodies();

		/**
		* \brief Clear all bodies.
		*
		* @param  NA
		* @return NA.
		*/
		void clearBodies();

		/**
		* \brief Add a body to message.
		*
		* Note: The ownership of the body will be transfered, user must NOT release it.
		* @param  A message body.
		* @return NA
		*/
		void addBody(EMMessageBody^ body);

		/**
		* \brief Add a extend attribute to message.
		*
		* Note: Supported types: bool, Int32, UInt32, Int64, UInt64, double. If the attribute has already existed, it will be replaced.
		* @param  The attrubute key.
		* @param  The attrubute value.
		* @return NA
		*/
		generic<typename T>
			void setAttribute(const String^ attribute, T value);

			void setAttribute(const String^ attribute, String^ value);

			/**
			* \brief Get extend attribute of message.
			*
			* Note: Supported types: bool, int, UInt32, long, UInt64, double, and String.
			* @param  The attrubute key.
			* @param  The attrubute value, it's a out argument.
			* @return Return false if attribute not exist or attribute type is wrong.
			*/
			bool getAttribute(const String^ attribute, [Out] bool% value);
			bool getAttribute(const String^ attribute, [Out] int% value);
			//bool getAttribute(const String^ attribute, [Out] UInt32 %value);
			bool getAttribute(const String^ attribute, [Out] Int64% value);
			//bool getAttribute(const String^ attribute, UInt64 %value);
			bool getAttribute(const String^ attribute, [Out] double% value);
			bool getAttribute(const String^ attribute, [Out] String^% value);

			/**
			* \brief Remove a attribute from message.
			*
			* @param  The attribute key.
			* @return NA
			*/
			void removeAttribute(const String^ attribute);

			/**
			* \brief Remove all attributes from message.
			*
			* @param  NA
			* @return NA
			*/
			void clearAttributes();

			/**
			* \brief Get all attributes from message.
			*
			* @param  NA
			* @return Attributes map.
			*/
			//const Dictionary<String^, EMAttributeValue^> ext();

			/**
			* \brief Get message's callback to notify status change.
			*
			* @param  NA
			* @return The callback.
			*/
			EMCallback^ callback();

			/**
			* \brief Set message's callback to notify status change.
			*
			* @param  The callback.
			* @return NA.
			*/
			void setCallback(EMCallback^);

			void setProgress(float percent);

			float getProgress();

	public:
		// factory method
		/**
		* \brief Create a received message.
		*
		* @param  The message sender.
		* @param  The message receiver.
		* @param  The message body.
		* @param  The message chat type.
		* @return A message instance.
		*/
		static EMMessage^ createReceiveMessage(String^ from, String^ to, EMMessageBody^ body, EMChatType chatType);

		/**
		* \brief Create a send message.
		*
		* @param  The message sender.
		* @param  The message receiver.
		* @param  The message body.
		* @param  The message chat type.
		* @return A message instance.
		*/
		static EMMessage^ createSendMessage(String^ from, String^ to, EMMessageBody^ body, EMChatType chatType);

	private:
		easemob::EMMessagePtr& getImpl();
	};

	typedef cli::array<EMMessage^> EMMessageList;
}

