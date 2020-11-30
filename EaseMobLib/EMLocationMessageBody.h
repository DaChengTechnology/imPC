#pragma once
#include "include/message/emlocationmessagebody.h"
#include "EMMessageBody.h"

namespace EaseMobLib {
	public ref class EMLocationMessageBody : EMMessageBody
	{
	public:
		const static float MSG_INVALID_LAT_LONG = -999.0;

		/**
		* \brief Location message body constructor.
		*
		* @param  Latitude.
		* @param  Longitude.
		* @param  The address.
		* @return NA
		*/
		EMLocationMessageBody(const double latitude, const double longitude, const String^ address);

		/**
		* \brief Copy constructor.
		*
		* @param  Another location message body.
		* @return NA
		*/
		EMLocationMessageBody(const EMLocationMessageBody^);

		/**
		* \brief Assign operator overload.
		*
		* @param  Another location message body.
		* @return The location message body.
		*/
		EMLocationMessageBody% operator=(const EMLocationMessageBody%);

		/**
		* \brief Class destructor.
		*
		* @param  NA
		* @return NA
		*/
		virtual ~EMLocationMessageBody();

		/**
		* \brief Get latitude.
		*
		* @param  NA
		* @return The latitude.
		*/
		double latitude();

		/**
		* \brief Get longitude.
		*
		* @param  NA
		* @return The longitude.
		*/
		double longitude();

		/**
		* \brief Get address.
		*
		* @param  NA
		* @return The address.
		*/
		const String^ address();

		/**
		* \brief Set latitude.
		*
		* @param latitude
		* @return NA
		*/
		void setLatitude(double);

		/**
		* \brief Set longitude.
		*
		* @param longitude
		* @return NA
		*/
		void setLongitude(double);

		/**
		* \brief Set address.
		*
		* @param address
		* @return NA
		*/
		void setAddress(const String^);
	private:
		easemob::EMLocationMessageBodyPtr& EMLocationMessageBody::getImpl();
	};
}

