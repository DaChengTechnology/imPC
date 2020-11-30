#pragma once
#include "include/message/emimagemessagebody.h"
#include "EMFileMessageBody.h"

namespace EaseMobLib {
	public ref class EMImageMessageBody :EMFileMessageBody
	{
	public:
		static ref class Size
		{
		public:
			Size(double width, double height) : mWidth(width), mHeight(height) {}
			double mWidth;
			double mHeight;
		};

		/**
		* \brief Image message body constructor.
		*
		* @param  NA
		* @return NA
		*/
		EMImageMessageBody();

		/**
		* \brief Image message body constructor.
		*
		* @param  Image attachment local path.
		* @param  Image thumbnail local path.
		* @return NA
		*/
		EMImageMessageBody(const String^ localPath, const String^ thumbnailLocalPath);

		/**
		* \brief Copy constructor.
		*
		* Note: Only copy the file path.
		* @param  Another image message body.
		* @return NA
		*/
		EMImageMessageBody(const EMImageMessageBody%);

		/**
		* \brief Assign operator overload.
		*
		* Note: Only copy the file path.
		* @param  Another image message body.
		* @return The image message body.
		*/
		EMImageMessageBody% operator=(const EMImageMessageBody%);

		/**
		* \brief Class destructor.
		*
		* @param  NA
		* @return NA
		*/
		virtual ~EMImageMessageBody();

		/**
		* \brief Set display name of the thumbnail.
		*
		* @param  The display name.
		* @return NA
		*/
		void setThumbnailDisplayName(const String^);

		/**
		* \brief Get display name of the thumbnail.
		*
		* @param  NA
		* @return The display name.
		*/
		const String^ thumbnailDisplayName();

		/**
		* \brief Set local path of the thumbnail.
		*
		* @param  The local path.
		* @return NA
		*/
		void setThumbnailLocalPath(const String^);

		/**
		* \brief Get local path of the thumbnail.
		*
		* @param  NA
		* @return The local path.
		*/
		const String^ thumbnailLocalPath();

		/**
		* \brief Set remote path of the thumbnail.
		*
		* Note: It's internal used, user should never need to call this method.
		* @param  The remote path.
		* @return NA
		*/
		void setThumbnailRemotePath(const String^);

		/**
		* \brief Get remote path of the thumbnail.
		*
		* @param  NA
		* @return The remote path.
		*/
		const String^ thumbnailRemotePath();

		/**
		* \brief Set secret key of the thumbnail.
		*
		* Note: It's internal used, user should never need to call this method.
		* @param  The secret key.
		* @return NA
		*/
		void setThumbnailSecretKey(const String^);

		/**
		* \brief Get secret key of the thumbnail.
		*
		* @param  NA
		* @return The secret key.
		*/
		const String^ thumbnailSecretKey();

		/**
		* \brief Set size of the thumbnail.
		*
		* @param  The thumbnail size.
		* @return NA
		*/
		void setThumbnailSize(const Size^);

		/**
		* \brief Get size of the thumbnail.
		*
		* @param  NA
		* @return The thumbnail size.
		*/
		Size^ thumbnailSize();

		/**
		* \brief Set file length of the thumbnail.
		*
		* Note: It's usually not necessary to call this method, will calculate file length automatically when setting local path.
		* @param  The file length.
		* @return NA
		*/
		void setThumbnailFileLength(int64_t);

		/**
		* \brief Get file length of the thumbnail.
		*
		* @param  NA
		* @return The file length.
		*/
		int64_t thumbnailFileLength();

		/**
		* \brief Set download status of the thumbnail.
		*
		* Note: Usually, user should NOT call this method directly.
		* @param  The download status.
		* @return NA
		*/
		void setThumbnailDownloadStatus(EaseMobLib::EMDownloadStatus);

		/**
		* \brief Get download status of the thumbnail.
		*
		* @param  NA
		* @return The download status.
		*/
		EaseMobLib::EMDownloadStatus thumbnailDownloadStatus();

		/**
		* \brief Set size of the image.
		*
		* @param  The image's size.
		* @return NA
		*/
		void setSize(const Size^);

		/**
		* \brief Get size of the image.
		*
		* @param  NA
		* @return The image size.
		*/
		const Size^ size();

	private:
		easemob::EMImageMessageBodyPtr& EMImageMessageBody::getImpl();
	};
}

