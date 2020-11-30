#include "include/message/emtextmessagebody.h"

#include "pch.h"
#include "EMTextMessageBody.h"
#include "EMUtils.h"

namespace EaseMobLib {
	/**
	* \brief Text message body constructor.
	*
	* @param  The text.
	* @return NA
	*/
	EMTextMessageBody::EMTextMessageBody(const String^ content) : EMMessageBody(EMMessageBodyType::TEXT) {
		easemob::EMTextMessageBodyPtr body(new easemob::EMTextMessageBody(extractCSString(content)));
		setNativeHandler<easemob::EMTextMessageBodyPtr>(&body);
	}

	/**
	* \brief Copy constructor.
	*
	* @param  Another text message body.
	* @return NA
	*/
	EMTextMessageBody::EMTextMessageBody(const EMTextMessageBody% body)
		: EMMessageBody(EMMessageBodyType::TEXT) {
		nativeInit<easemob::EMTextMessageBodyPtr>(% body);
	}

	/**
	* \brief Assign operator overload.
	*
	* @param  Another text message body.
	* @return The text message body.
	*/
	EMTextMessageBody% EMTextMessageBody::operator = (const EMTextMessageBody% body) {
		nativeInit<easemob::EMTextMessageBodyPtr>(% body);
		return *this;
	}

	/**
	* \brief Class destructor.
	*
	* @param  NA
	* @return NA
	*/
	EMTextMessageBody::~EMTextMessageBody() {
		nativeFinalize<easemob::EMTextMessageBodyPtr>();
	}

	/**
	* \brief Get the text.
	*
	* @param  NA
	* @return The text.
	*/
	const String^ EMTextMessageBody::text() {
		return getCSString(getImpl()->text());
	}

	easemob::EMTextMessageBodyPtr& EMTextMessageBody::getImpl() {
		return getNative<easemob::EMTextMessageBodyPtr>();
	}

}
