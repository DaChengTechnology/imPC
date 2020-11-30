#include "pch.h"
#include "EMMessage.h"
#include "EMUtils.h"

namespace EaseMobLib {
	/**
	* \brief Class destructor.
	*
	* @param  NA
	* @return NA
	*/
	EMMessage::~EMMessage() {
		nativeFinalize<easemob::EMMessagePtr>();
	}

	/**
	* \brief Get message id.
	*
	* @param  NA
	* @return The message id.
	*/
	const String^ EMMessage::msgId() {
		return getCSString(getImpl()->msgId());
	}

	/**
	* \brief Set message id.
	*
	* Note: User should never change a message's id if you don't want to save as a new message.
	* @param  The new message id.
	* @return NA.
	*/
	void EMMessage::setMsgId(const String^ msgId) {
		getImpl()->setMsgId(extractCSString(msgId));
	}

	/**
	* \brief Get message sender.
	*
	* @param  NA
	* @return The message sender.
	*/
	const String^ EMMessage::from() {
		return getCSString(getImpl()->from());
	}

	/**
	* \brief Set message sender.
	*
	* @param  The new message sender.
	* @return NA.
	*/
	void EMMessage::setFrom(const String^ from) {
		getImpl()->setFrom(extractCSString(from));
	}

	/**
	* \brief Get message receiver.
	*
	* @param  NA
	* @return The message receiver.
	*/
	const String^ EMMessage::to() {
		return getCSString(getImpl()->to());
	}

	/**
	* \brief Set message receiver.
	*
	* @param  The new message receiver.
	* @return NA.
	*/
	void EMMessage::setTo(const String^ to) {
		getImpl()->setTo(extractCSString(to));
	}

	/**
	* \brief Get conversation id.
	*
	* @param  NA
	* @return The conversation id.
	*/
	const String^ EMMessage::conversationId() {
		return getCSString(getImpl()->conversationId());
	}

	/**
	* \brief Set message's conversation id.
	*
	* Note: User should NOT change message's conversation id after receive or send a message.
	* @param  The new conversation id.
	* @return NA.
	*/
	void EMMessage::setConversationId(const String^ id) {
		getImpl()->setConversationId(extractCSString(id));
	}

	/**
	* \brief Get message status.
	*
	* @param  NA
	* @return The message status.
	*/
	EMMessageStatus EMMessage::status() {
		/*
		easemob::EMMessage::EMMessageStatus status = getImpl()->status();
		switch (status) {
		case easemob::EMMessage::NEW:
			return EMMessageStatus::NEW;
		case easemob::EMMessage::DELIVERING:
			return EMMessageStatus::DELIVERING;
		case easemob::EMMessage::SUCCESS:
			return EMMessageStatus::SUCCESS;
		case easemob::EMMessage::FAIL:
			return EMMessageStatus::FAIL;
		}*/
		return (EMMessageStatus)getImpl()->status();
	}

	/**
	* \brief Set message's status.
	*
	* Note: User should NOT change message's status directly.
	* @param  The new message status.
	* @return NA.
	*/
	void EMMessage::setStatus(EMMessageStatus status) {
		/*
		switch (status) {
		case EMMessageStatus::NEW:
			getImpl()->setStatus(easemob::EMMessage::NEW);
			break;
		case EMMessageStatus::DELIVERING:
			getImpl()->setStatus(easemob::EMMessage::DELIVERING);
			break;
		case EMMessageStatus::SUCCESS:
			getImpl()->setStatus(easemob::EMMessage::SUCCESS);
			break;
		case EMMessageStatus::FAIL:
			getImpl()->setStatus(easemob::EMMessage::FAIL);
			break;
		}*/
		getImpl()->setStatus((easemob::EMMessage::EMMessageStatus)status);
	}

	/**
	* \brief Get message chat type.
	*
	* @param  NA
	* @return The message chat type.
	*/
	EMChatType EMMessage::chatType() {
		/*
		easemob::EMMessage::EMChatType chatType = getImpl()->chatType();
		switch (chatType) {
		case easemob::EMMessage::SINGLE:
			return EMChatType::SINGLE;
		case easemob::EMMessage::GROUP:
			return EMChatType::GROUP;
		case easemob::EMMessage::CHATROOM:
			return EMChatType::CHATROOM;
		}
		*/
		return (EMChatType)getImpl()->chatType();
	}

	/**
	* \brief Set message's chat type.
	*
	* Note: User should NOT change message's chat type after receive or send a message.
	* @param  The new conversation id.
	* @return NA.
	*/
	void EMMessage::setChatType(EMChatType chatType) {
		/*
		switch (chatType) {
		case EMChatType::SINGLE:
			getImpl()->setChatType(easemob::EMMessage::SINGLE);
			break;
		case EMChatType::GROUP:
			getImpl()->setChatType(easemob::EMMessage::GROUP);
			break;
		case EMChatType::CHATROOM:
			getImpl()->setChatType(easemob::EMMessage::CHATROOM);
			break;
		}*/
		getImpl()->setChatType((easemob::EMMessage::EMChatType)chatType);
	}

	/**
	* \brief Get message direction.
	*
	* @param  NA
	* @return The message direction.
	*/
	EMMessageDirection EMMessage::msgDirection() {
		return (EMMessageDirection)getImpl()->msgDirection();
	}

	/**
	* \brief Set message's direction.
	*
	* Note: User should NOT change message's message direction after receive or send a message.
	* @param  NA
	* @return NA.
	*/
	void EMMessage::setMsgDirection(EMMessageDirection dir) {
		getImpl()->setMsgDirection((easemob::EMMessage::EMMessageDirection)dir);
	}

	/**
	* \brief Get message if has read status.
	*
	* @param  NA
	* @return The message read status.
	*/
	bool EMMessage::isRead() {
		return getImpl()->isRead();
	}

	/**
	* \brief Set message's read status.
	*
	* Note: User should NOT change message's read status directly.
	* @param  The new message read status.
	* @return NA.
	*/
	void EMMessage::setIsRead(bool isRead) {
		getImpl()->setIsRead(isRead);
	}

	/**
	* \brief Get message if has listened status.
	*
	* @param  NA
	* @return The message listened status.
	*/
	bool EMMessage::isListened() {
		return getImpl()->isListened();
	}

	/**
	* \brief Set message's listened status.
	*
	* Note: User should NOT change message's listened status directly.
	* @param  The new message listened status.
	* @return NA.
	*/
	void EMMessage::setIsListened(bool isListened) {
		getImpl()->setIsListened(isListened);
	}

	/**
	* \brief Get message read ack status.
	*
	* Note: For receiver, it indicates whether has sent read ack, and for sender, it indicates whether has received read ack.
	* @param  NA
	* @return The message read ack status.
	*/
	bool EMMessage::isReadAcked() {
		return getImpl()->isReadAcked();
	}

	/**
	* \brief Set message's read ack status.
	*
	* Note: User should NOT change message's read ack status directly.
	* @param  The new message read ack status.
	* @return NA.
	*/
	void EMMessage::setIsReadAcked(bool acked) {
		getImpl()->setIsReadAcked(acked);
	}

	/**
	* \brief Get message delivering status.
	*
	* Note: For receiver, it indicates whether has sent delivering successed ack, and for sender, it indicates whether has received delivering successed ack.
	* @param  NA
	* @return The message delivering status.
	*/
	bool EMMessage::isDeliverAcked() {
		return  getImpl()->isDeliverAcked();
	}

	/**
	* \brief Set message's delivery ack status.
	*
	* Note: User should NOT change message's delivery ack status directly.
	* @param  The new message delivery ack status.
	* @return NA.
	*/
	void EMMessage::setIsDeliverAcked(bool deliverAcked) {
		getImpl()->setIsDeliverAcked(deliverAcked);
	}

	/**
	* \brief Get message timestamp.
	*
	* @param  NA
	* @return The message timestamp.
	*/
	int64_t EMMessage::timestamp() {
		return getImpl()->timestamp();
	}

	/**
	* \brief Set message's timestamp.
	*
	* Note: User should NOT change message's timestamp.
	* @param  The new message timestamp.
	* @return NA.
	*/
	void EMMessage::setTimestamp(int64_t timeStamp) {
		getImpl()->setTimestamp(timeStamp);
	}

	/**
	* \brief Get message body list.
	*
	* @param  NA
	* @return The message body list.
	*/
	array<EMMessageBody^>^ EMMessage::bodies() {
		std::vector<easemob::EMMessageBodyPtr> bodies = getImpl()->bodies();
		array<EMMessageBody^>^ csBodies = gcnew array<EMMessageBody^>(bodies.size());
		int i = 0;
		for (easemob::EMMessageBodyPtr body : bodies) {
			EMMessageBody^ csBody;
			switch (body->type()) {
			case easemob::EMMessageBody::TEXT: {
				easemob::EMTextMessageBodyPtr* ptr = new easemob::EMTextMessageBodyPtr(
					std::static_pointer_cast<easemob::EMTextMessageBody, easemob::EMMessageBody>(body));
				csBody = getCSTextMessageBody(ptr);
				break;
			}
			case easemob::EMMessageBody::COMMAND: {
				easemob::EMCmdMessageBodyPtr* ptr = new easemob::EMCmdMessageBodyPtr(
					std::static_pointer_cast<easemob::EMCmdMessageBody, easemob::EMMessageBody>(body));
				csBody = getCSCmdMessageBody(ptr);
				break;
			}
			case easemob::EMMessageBody::IMAGE: {
				easemob::EMImageMessageBodyPtr* ptr = new easemob::EMImageMessageBodyPtr(
					std::static_pointer_cast<easemob::EMImageMessageBody, easemob::EMMessageBody>(body));
				csBody = getCSImageMessageBody(ptr);
				break;
			}
			case easemob::EMMessageBody::FILE: {
				easemob::EMFileMessageBodyPtr* ptr = new easemob::EMFileMessageBodyPtr(
					std::static_pointer_cast<easemob::EMFileMessageBody, easemob::EMMessageBody>(body));
				csBody = getCSFileMessageBody(ptr);
				break;
			}
			case easemob::EMMessageBody::VIDEO: {
				easemob::EMVideoMessageBodyPtr* ptr = new easemob::EMVideoMessageBodyPtr(
					std::static_pointer_cast<easemob::EMVideoMessageBody, easemob::EMMessageBody>(body));
				csBody = getCSVideoMessageBody(ptr);
				break;
			}
			case easemob::EMMessageBody::VOICE: {
				easemob::EMVoiceMessageBodyPtr* ptr = new easemob::EMVoiceMessageBodyPtr(
					std::static_pointer_cast<easemob::EMVoiceMessageBody, easemob::EMMessageBody>(body));
				csBody = getCSVoiceMessageBody(ptr);
				break;
			}
			case easemob::EMMessageBody::LOCATION: {
				easemob::EMLocationMessageBodyPtr* ptr = new easemob::EMLocationMessageBodyPtr(
					std::static_pointer_cast<easemob::EMLocationMessageBody, easemob::EMMessageBody>(body));
				csBody = getCSLocationMessageBody(ptr);
				break;
			}
			}
			csBodies[i] = csBody;
			i++;
		}
		return csBodies;
	}

	/**
	* \brief Clear all bodies.
	*
	* @param  NA
	* @return NA.
	*/
	void EMMessage::clearBodies() {
		getNative<easemob::EMMessagePtr>()->clearBodies();
	}

	/**
	* \brief Add a body to message.
	*
	* Note: The ownership of the body will be transfered, user must NOT release it.
	* @param  A message body.
	* @return NA
	*/
	void EMMessage::addBody(EMMessageBody^ body) {
		getImpl()->addBody(body->getNative<easemob::EMMessageBodyPtr>());
	}

	/**
	* \brief Add a extend attribute to message.
	*
	* Note: Supported types: bool, int32, uint32, int64, uint64, double, String. If the attribute has already existed, it will be replaced.
	* @param  The attrubute key.
	* @param  The attrubute value.
	* @return NA
	*/
	generic<typename T>
		void EMMessage::setAttribute(const String^ attribute, T value) {
			if (T::typeid == bool::typeid) {
				bool v = !!value;
				getImpl()->setAttribute(extractCSString(attribute), v);
			}
			else if (T::typeid == int::typeid) {
				int v = !!value;
				getImpl()->setAttribute(extractCSString(attribute), v);
			}
			else if (T::typeid == UInt32::typeid) {
				uint32_t v = !!value;
				getImpl()->setAttribute(extractCSString(attribute), v);
			}
			else if (T::typeid == long::typeid) {
				int64_t v = !!value;
				getImpl()->setAttribute(extractCSString(attribute), v);
			}
			else if (T::typeid == double::typeid) {
				double v = !!value;
				getImpl()->setAttribute(extractCSString(attribute), v);
			}
		}

		void EMMessage::setAttribute(const String^ attribute, String^ value) {
			getImpl()->setAttribute(extractCSString(attribute), extractCSString(value));
		}

		/**
		* \brief Get extend attribute of message.
		*
		* Note: Supported types: bool, int, unsigned int, int64_t and string.
		* @param  The attrubute key.
		* @param  The attrubute value, it's a out argument.
		* @return Return false if attribute not exist or attribute type is wrong.
		*/

		bool EMMessage::getAttribute(const String^ attribute, [Out] bool% value) {
			bool v;
			bool exists = getImpl()->getAttribute(extractCSString(attribute), v);
			if (exists) {
				value = v;
			}
			return exists;
		}
		bool EMMessage::getAttribute(const String^ attribute, [Out] int% value) {
			int v;
			bool exists = getImpl()->getAttribute(extractCSString(attribute), v);
			if (exists) {
				value = v;
			}
			return exists;
		}

		/*
		bool EMMessage::getAttribute(const String^ attribute, [Out] UInt32 %value) {
			UInt32 v;
			bool exists = getImpl()->getAttribute(extractCSString(attribute), v);
			if (exists) {
				value = v;
			}
			return exists;
		}
		*/
		bool EMMessage::getAttribute(const String^ attribute, [Out] Int64% value) {
			int64_t v;
			bool exists = getImpl()->getAttribute(extractCSString(attribute), v);
			if (exists) {
				value = v;
			}
			return exists;
		}
		/*
		bool EMMessage::getAttribute(const String^ attribute, UInt64 %value) {
			uint64_t v;
			bool exists = getImpl()->getAttribute(extractCSString(attribute), v);
			if (exists) {
				value = v;
			}
			return exists;
		}
		*/
		bool EMMessage::getAttribute(const String^ attribute, [Out] double% value) {
			double v;
			bool exists = getImpl()->getAttribute(extractCSString(attribute), v);
			if (exists) {
				value = v;
			}
			return exists;
		}

		bool EMMessage::getAttribute(const String^ attribute, [Out] String^% value) {
			std::string v;
			bool exists = getImpl()->getAttribute(extractCSString(attribute), v);
			if (exists) {
				value = getCSString(v);
			}
			return exists;
		}

		/**
		* \brief Remove a attribute from message.
		*
		* @param  The attribute key.
		* @return NA
		*/
		void EMMessage::removeAttribute(const String^ attribute) {
			getImpl()->removeAttribute(extractCSString(attribute));
		}

		/**
		* \brief Remove all attributes from message.
		*
		* @param  NA
		* @return NA
		*/
		void EMMessage::clearAttributes() {
			getImpl()->clearAttributes();
		}

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
		EMCallback^ EMMessage::callback() {
			// TODO
			return nullptr;
		}

		/**
		* \brief Set message's callback to notify status change.
		*
		* @param  The callback.
		* @return NA.
		*/
		void EMMessage::setCallback(EMCallback^ csCallback) {
			easemob::EMCallbackPtr* callback = &csCallback->getNative<easemob::EMCallbackPtr>();
			getImpl()->setCallback(*callback);
		}

		void EMMessage::setProgress(float percent) {
			getImpl()->setProgress(percent);
		}

		float EMMessage::getProgress() {
			return getImpl()->getProgress();
		}

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
		EMMessage^ EMMessage::createReceiveMessage(String^ from, String^ to, EMMessageBody^ body, EMChatType chatType) {
			easemob::EMMessagePtr ptr = easemob::EMMessage::createReceiveMessage(extractCSString(from), extractCSString(to),
				body == nullptr ? easemob::EMMessageBodyPtr() : body->getNative<easemob::EMMessageBodyPtr>(),
				(easemob::EMMessage::EMChatType)chatType);
			EMMessage^ msg = gcnew EMMessage();
			msg->setNativeHandler<easemob::EMMessagePtr>(&ptr);
			return msg;
		}

		/**
		* \brief Create a send message.
		*
		* @param  The message sender.
		* @param  The message receiver.
		* @param  The message body.
		* @param  The message chat type.
		* @return A message instance.
		*/
		EMMessage^ EMMessage::createSendMessage(String^ from, String^ to, EMMessageBody^ body, EMChatType chatType) {
			easemob::EMMessagePtr ptr = easemob::EMMessage::createSendMessage(extractCSString(from), extractCSString(to),
				body == nullptr ? easemob::EMMessageBodyPtr() : body->getNative<easemob::EMMessageBodyPtr>(),
				(easemob::EMMessage::EMChatType)chatType);
			EMMessage^ msg = gcnew EMMessage();
			msg->setNativeHandler<easemob::EMMessagePtr>(&ptr);
			return msg;
		}

		easemob::EMMessagePtr& EMMessage::getImpl() {
			return getNative<easemob::EMMessagePtr>();
		}
}