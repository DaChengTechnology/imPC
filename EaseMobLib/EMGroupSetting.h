#pragma once
#include "emsbsase.h"
#include <emgroupsetting.h>

namespace EaseMobLib {
	public enum class EMGroupStyle
	{
		PRIVATE_OWNER_INVITE,   //Private group, only group owner can invite user to the group
		PRIVATE_MEMBER_INVITE,  //Private group, both group owner and members can invite user to the group
		PUBLIC_JOIN_APPROVAL,   //Public group, user can apply to join the group, but need group owner's approval, and owner can invite user to the group
		PUBLIC_JOIN_OPEN,       //Public group, any user can freely join the group, and owner can invite user to the group
		PUBLIC_ANONYMOUS,       //Anonymous group, NOT support now
		DEFAUT = PRIVATE_OWNER_INVITE
	};
	public ref class EMGroupSetting : EMBase
	{
	public:
		Void setStyle(EMGroupStyle style) {
			getImpl()->setStyle((easemob::EMGroupSetting::EMGroupStyle)style);
		}

		EMGroupStyle style() {
			easemob::EMGroupSetting::EMGroupStyle s = getImpl()->style();
			return (EMGroupStyle)s;
		}

		const int maxCount() {
			return (int)getImpl()->maxUserCount();
		}

		Void setMaxCount(int count) {
			getImpl()->setMaxUserCount(count);
		}

	private:
		easemob::EMGroupSettingPtr& getImpl();
	};

	inline easemob::EMGroupSettingPtr& EMGroupSetting::getImpl()
	{
		return getNative<easemob::EMGroupSettingPtr>();
	}

}