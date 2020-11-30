#include "pch.h"
#include "EMLocationMessageBody.h"
#include "EMUtils.h"

namespace EaseMobLib {
	/**
	* \brief Location message body constructor.
	*
	* @param  Latitude.
	* @param  Longitude.
	* @param  The address.
	* @return NA
	*/
	EMLocationMessageBody::EMLocationMessageBody(const double latitude, const double longitude, const String^ address): EMMessageBody(EMMessageBodyType::LOCATION) {
		easemob::EMLocationMessageBodyPtr body(new easemob::EMLocationMessageBody(latitude, longitude, extractCSString(address)));
		setNativeHandler<easemob::EMLocationMessageBodyPtr>(&body);
	}

	/**
	* \brief Copy constructor.
	*
	* @param  Another location message body.
	* @return NA
	*/
	EMLocationMessageBody::EMLocationMessageBody(const EMLocationMessageBody^ body) : EMMessageBody(EMMessageBodyType::LOCATION) {
		nativeInit<easemob::EMLocationMessageBodyPtr>(body);
	}

	/**
	* \brief Assign operator overload.
	*
	* @param  Another location message body.
	* @return The location message body.
	*/
	EMLocationMessageBody% EMLocationMessageBody::operator = (const EMLocationMessageBody% body) {
		nativeInit<easemob::EMLocationMessageBodyPtr>(% body);
		return *this;
	}

	/**
	* \brief Class destructor.
	*
	* @param  NA
	* @return NA
	*/
	EMLocationMessageBody::~EMLocationMessageBody() {
		nativeFinalize<easemob::EMLocationMessageBodyPtr>();
	}

	/**
	* \brief Get latitude.
	*
	* @param  NA
	* @return The latitude.
	*/
	double EMLocationMessageBody::latitude() {
		return getImpl()->latitude();
	}

	/**
	* \brief Get longitude.
	*
	* @param  NA
	* @return The longitude.
	*/
	double EMLocationMessageBody::longitude() {
		return getImpl()->longitude();
	}

	/**
	* \brief Get address.
	*
	* @param  NA
	* @return The address.
	*/
	const String^ EMLocationMessageBody::address() {
		return getCSString(getImpl()->address());
	}

	/**
	* \brief Set latitude.
	*
	* @param latitude
	* @return NA
	*/
	void EMLocationMessageBody::setLatitude(double lat) {
		getImpl()->setLatitude(lat);
	}

	/**
	* \brief Set longitude.
	*
	* @param longitude
	* @return NA
	*/
	void EMLocationMessageBody::setLongitude(double longitude) {
		getImpl()->setLongitude(longitude);
	}

	/**
	* \brief Set address.
	*
	* @param address
	* @return NA
	*/
	void EMLocationMessageBody::setAddress(const String^ addr) {
		getImpl()->setAddress(extractCSString(addr));
	}

	easemob::EMLocationMessageBodyPtr& EMLocationMessageBody::getImpl() {
		return getNative<easemob::EMLocationMessageBodyPtr>();
	}
}