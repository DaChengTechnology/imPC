#pragma once
#include "include/message/emvideomessagebody.h"
#include "EMFileMessageBody.h"

namespace EaseMobLib {
	public ref class EMVideoMessageBody : EMFileMessageBody
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
		* \brief Video message body constructor.
		*
		* @param  NA
		* @return NA
		*/
		EMVideoMessageBody();

		/**
		* \brief Video message body constructor.
		*
		* @param  Video attachment local path.
		* @param  Video thumbnail local path.
		* @return NA
		*/
		EMVideoMessageBody(const String^ localPath, const String^ thumbnailLocalPath);

		/**
		* \brief Copy constructor.
		*
		* Note: Only copy the file path.
		* @param  Another video message body.
		* @return NA
		*/
		EMVideoMessageBody(const EMVideoMessageBody^);

		/**
		* \brief Assign operator overload.
		*
		* Note: Only copy the file path.
		* @param  Another video message body.
		* @return The image message body.
		*/
		EMVideoMessageBody% operator=(const EMVideoMessageBody%);

		/**
		* \brief Class destructor.
		*
		* @param  NA
		* @return NA
		*/
		virtual ~EMVideoMessageBody();

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
		* \brief Set download status of the thumbnail.
		*
		* Note: Usually, user should NOT call this method directly.
		* @param  The download status.
		* @return NA
		*/
		void setThumbnailDownloadStatus(EMDownloadStatus);

		/**
		* \brief Get download status of the thumbnail.
		*
		* @param  NA
		* @return The download status.
		*/
		EMDownloadStatus thumbnailDownloadStatus();

		/**
		* \brief Set size of the video.
		*
		* @param  The video's size.
		* @return NA
		*/
		void setSize(const Size^);

		/**
		* \brief Get size of the video.
		*
		* @param  NA
		* @return The video size.
		*/
		const Size^ size();

		/**
		* \brief Get playing duration of the video.
		*
		* @param  NA
		* @return The video playing duration.
		*/
		int duration();

		/**
		* \brief Set playing duration of the video.
		*
		* @param  The video's playing duration.
		* @return NA
		*/
		void setDuration(int);
	private:
		easemob::EMVideoMessageBodyPtr& getImpl();
	};
}

