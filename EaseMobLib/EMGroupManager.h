#pragma once
#include <emgroupmanager_interface.h>
#include "EMGroup.h"
#include "EMCursorResult.h"
#include "EMGroupSetting.h"
namespace EaseMobLib {
	// EMError
	public ref class EMGroupManagerListener : public EMBase {
	public:
		/**
		* \brief Callback user when user is invited to a group.
		*
		* Note: User can accept or decline the invitation.
		* @param  The group that invite the user.
		* @param  The inviter.
		* @param  The invite message.
		* @return NA
		*/
		delegate void D_onReceiveInviteFromGroup(const String^ groupId, const String^ inviter, const String^ inviteMessage);

		/**
		* \brief Callback user when the user accept to join the group.
		*
		* @param  The group that invite the user.
		* @return NA
		*/
		delegate void D_onReceiveInviteAcceptionFromGroup(const EMGroup^ group, const String^ invitee);

		/**
		* \brief Callback user when the user decline to join the group.
		*
		* @param  The group that invite the user.
		* @param  User's decline reason.
		* @return NA
		*/
		delegate void D_onReceiveInviteDeclineFromGroup(const EMGroup^ group, const String^ invitee, const String^ reason);

		/**
		* \brief Callback user when user is invited to a group.
		*
		* Note: User has been added to the group when received this callback.
		* @param  The group that invite the user.
		* @param  The inviter.
		* @param  The invite message.
		* @return NA
		*/
		delegate void D_onAutoAcceptInvitationFromGroup(const EMGroup^ group, const String^ inviter, const String^ inviteMessage);

		/**
		* \brief Callback user when user is kicked out from a group or the group is destroyed.
		*
		* @param  The group that user left.
		* @param  The leave reason.
		* @return NA
		*/
		delegate void D_onLeaveGroup(const EMGroup^ group, EMGroupLeaveReason reason);

		/**
		* \brief Callback user when receive a join group application.
		*
		* @param  The group that user try to join.
		* @param  User that try to join the group.
		* @param  The apply message.
		* @return NA
		*/
		delegate void D_onReceiveJoinGroupApplication(const EMGroup^ group, const String^ from, const String^ message);

		/**
		* \brief Callback user when receive owner's approval.
		*
		* @param  The group to join.
		* @return NA
		*/
		delegate void D_onReceiveAcceptionFromGroup(const EMGroup^ group);

		/**
		* \brief Callback user when receive group owner's rejection.
		*
		* @param  The group that user try to join.
		* @param  Owner's reject reason.
		* @return NA
		*/
		delegate void D_onReceiveRejectionFromGroup(const String^ groupId, const String^ reason);

		/**
		* \brief Callback user when login user's group list is updated.
		*
		* @param  The login user's group list.
		* @return NA
		*/
		delegate void D_onUpdateMyGroupList(array<EMGroup^>^ list);

		/**
		* \brief Callback user when user add to group mute list.
		*
		* @param  The group that add user to mute list.
		* @return NA
		*/
		delegate void D_onAddMutesFromGroup(const EMGroup^ group, array<String^>^ mutes, int64_t muteExpire);

		/**
		* \brief Callback user when user remove from group mute list.
		*
		* @param  The group that remove user from mute list.
		* @return NA
		*/
		delegate void D_onRemoveMutesFromGroup(const EMGroup^ group, array<String^>^ mutes);

		/**
		* \brief Callback user when promote to group admin.
		*
		* @param  The group that promote user admin.
		* @return NA
		*/
		delegate void D_onAddAdminFromGroup(const EMGroup^ group, const String^ admin);

		/**
		* \brief Callback user when cancel admin.
		*
		* @param  The group that cancel user admin.
		* @return NA
		*/
		delegate void D_onRemoveAdminFromGroup(const EMGroup^ group, const String^ admin);

		/**
		* \brief Callback user when promote to group owner.
		*
		* @param  The group that promote user to owner.
		* @return NA
		*/
		delegate void D_onAssignOwnerFromGroup(const EMGroup^ group, const String^ newOwner, const String^ oldOwner);

		/**
		* \brief Callback user when a user join the group.
		*
		* @param  The group that user joined.
		* @param  The member.
		* @return NA
		*/
		delegate void D_onMemberJoinedGroup(const EMGroup^ group, const String^ member);

		/**
		* \brief Callback user when a user leave the group.
		*
		* @param  The group that user left.
		* @param  The member.
		* @return NA
		*/
		delegate void D_onMemberLeftGroup(const EMGroup^ group, const String^ member);

		/**
		* \brief Callback user when update group announcement.
		*
		* @param  The group that update the announcement.
		* @param  The announcement.
		* @return NA
		*/
		delegate void D_onUpdateAnnouncementFromGroup(const EMGroup^ group, const String^ announcement);

		/**
		* \brief Callback user when group admin or owner or file uploader delete share file.
		*
		* @param  The group that delete share file.
		* @param  The delete share file id.
		* @return NA
		*/
		delegate void D_onDeleteSharedFileFromGroup(const EMGroup^ group, const String^ fileId);
	public:
		D_onReceiveInviteFromGroup^ onReceiveInviteFromGroup = nullptr;
		D_onReceiveInviteAcceptionFromGroup^ onReceiveInviteAcceptionFromGroup = nullptr;
		D_onReceiveInviteDeclineFromGroup^ onReceiveInviteDeclineFromGroup = nullptr;
		D_onAutoAcceptInvitationFromGroup^ onAutoAcceptInvitationFromGroup = nullptr;
		D_onLeaveGroup^ onLeaveGroup = nullptr;
		D_onReceiveJoinGroupApplication^ onReceiveJoinGroupApplication = nullptr;
		D_onReceiveAcceptionFromGroup^ onReceiveAcceptionFromGroup = nullptr;
		D_onReceiveRejectionFromGroup^ onReceiveRejectionFromGroup = nullptr;
		D_onUpdateMyGroupList^ onUpdateMyGroupList = nullptr;
		D_onAddMutesFromGroup^ onAddMutesFromGroup = nullptr;
		D_onRemoveMutesFromGroup^ onRemoveMutesFromGroup = nullptr;
		D_onAddAdminFromGroup^ onAddAdminFromGroup = nullptr;
		D_onRemoveAdminFromGroup^ onRemoveAdminFromGroup = nullptr;
		D_onAssignOwnerFromGroup^ onAssignOwnerFromGroup = nullptr;
		D_onMemberJoinedGroup^ onMemberJoinedGroup = nullptr;
		D_onMemberLeftGroup^ onMemberLeftGroup = nullptr;
		D_onUpdateAnnouncementFromGroup^ onUpdateAnnouncementFromGroup = nullptr;
		D_onDeleteSharedFileFromGroup^ onDeleteSharedFileFromGroup = nullptr;
	};
	public ref class EMGroupManager
	{
	public:

		/**
		* \brief Constructor of EMGroupManager
		*
		* @param  native group manger interface pointer.
		* @return NA
		*/
		EMGroupManager(easemob::EMGroupManagerInterface*);
		~EMGroupManager();

		/**
		* \brief Add a listener to group manager.
		*
		* @param  A group manager listener.
		* @return NA
		*/
		void addListener(EMGroupManagerListener^);

		/**
		* \brief Remove a listener.
		*
		* @param  A group manager listener.
		* @return NA
		*/
		void removeListener(EMGroupManagerListener^);

		/**
		* \brief Remove all the listeners.
		*
		* @param  NA
		* @return NA
		*/
		void clearListeners();

		/**
		* \brief Get a group with groupId, create the group if not exist.
		*
		* @param  Group's id.
		* @return A group with the groupId.
		*/
		EMGroup^ groupWithId(const String^ groupId);

		/**
		* \brief Get groups for login user from memory.
		*
		* @param  EMError used for output.
		* @return All user joined groups in memory.
		*/
		EMGroupList^ allMyGroups(EMError^ error);

		/**
		* \brief Get groups for login user from db.
		*
		* @return All user joined groups in db.
		*/
		EMGroupList^ loadAllMyGroupsFromDB();

		/**
		* \brief Fetch all groups for login user from server.
		*
		* Note: Groups in memory will be updated.
		* @param  EMError used for output.
		* @return All user joined groups.
		*/
		EMGroupList^ fetchAllMyGroups(EMError^ error);

		/**
		* \brief Fetch all groups for login user from server with page.
		*
		* Note: User can input 1 as first page number at the first time.
		* @param  Page's number.
		* @param  Page's size.
		* @param  EMError used for output.
		* @return current page's user joined groups.
		*/
		EMGroupList^ fetchAllMyGroupsWithPage(
			int pageNum,
			int pageSize,
			EMError^ error);

		/**
		* \brief Fetch app's public groups.
		*
		* Note: User can input empty string as cursor at the first time.
		* @param  Page's cursor.
		* @param  Page's size.
		* @param  EMError used for output.
		* @return Wrapper of next page's cursor and current page result.
		*/
		EMCursorResult^ fetchPublicGroupsWithCursor(
			const String^ cursor,
			int pageSize,
			EMError^ error);

		/**
		* \brief Create a new group.
		*
		* Note: Login user will be the owner of created .
		* @param  Group's subject.
		* @param  Group's description.
		* @param  Welcome message that will be sent to invited user.
		* @param  Group's setting.
		* @param  Group's members.
		* @param  EMError used for output.
		* @return The created group.
		*/
		EMGroup^ createGroup(
			const String^ subject,
			const String^ description,
			const String^ welcomeMessage,
			EMGroupSetting^ setting,
			cli::array<String^>^ members,
			EMError^ error);

		/**
		* \brief Join a public group.
		*
		* Note: The group's style must be PUBLIC_JOIN_OPEN, or will return error.
		* @param  Group's ID.
		* @param  EMError used for output.
		* @return The joined group.
		*/
		EMGroup^ joinPublicGroup(
			const String^ groupId,
			EMError^ error
		);

		/**
		* \brief Apply to join a public group, need owner's approval.
		*
		* Note: The group's style must be PUBLIC_JOIN_APPROVAL, or will return error.
		* @param  Group's ID.
		* @param  Nick name in the group.
		* @param  Apply message, that will be sent to group owner.
		* @param  EMError used for output.
		* @return The group that try to join.
		*/
		EMGroup^ applyJoinPublicGroup(
			const String^ groupId,
			const String^ nickName,
			const String^ message,
			EMError^ error
		);

		/**
		* \brief Leave a group.
		*
		* Note: Group's owner can't leave the group.
		* @param  Group's ID.
		* @param  EMError used for output.
		* @return The leaved group.
		*/
		void leaveGroup(
			const String^ groupId,
			EMError^ error
		);

		/**
		* \brief Destroy a group.
		*
		* Note: Only group's owner can destroy the group.
		* @param  Group's ID.
		* @param  EMError used for output.
		* @return The destroyed group.
		*/
		void destroyGroup(
			const String^ groupId,
			EMError^ error
		);

		/**
		* \brief Add members to a group.
		*
		* Note: Maybe user don't have the priviledge, it depends on group's setting.
		* @param  Group's ID.
		* @param  Invited users.
		* @param  Welcome message that will be sent to invited user.
		* @param  EMError used for output.
		* @return The group that added members.
		*/
		EMGroup^ addGroupMembers(
			const String^ groupId,
			cli::array<String^>^ members,
			const String^ welcomeMessage,
			EMError^ error
		);

		/**
		* \brief Remove members from a group.
		*
		* Note: Only group's owner can remove members.
		* @param  Group's ID.
		* @param  Removed users.
		* @param  EMError used for output.
		* @return The group that removed members.
		*/
		EMGroup^ removeGroupMembers(
			const String^ groupId,
			cli::array<String^>^ members,
			EMError^ error
		);

		/**
		* \brief Block group's members, the blocked user can't send message in the group.
		*
		* Note: Only group's owner can block other members.
		* @param  Group's ID.
		* @param  Blocked users.
		* @param  EMError used for output.
		* @param  The reason that why block the members.
		* @return The group that blocked members.
		*/
		EMGroup^ blockGroupMembers(
			const String^ groupId,
			cli::array<String^>^ members,
			EMError^ error,
			const String^ reason
		);

		/**
		* \brief Unblock group's members.
		*
		* Note: Only group's owner can unblock other members.
		* @param  Group's ID.
		* @param  Unblocked users.
		* @param  EMError used for output.
		* @return The group that unblocked members.
		*/
		EMGroup^ unblockGroupMembers(
			const String^ groupId,
			cli::array<String^>^ members,
			EMError^ error
		);

		/**
		* \brief Change group's subject.
		*
		* Note: Only group's owner can change group's subject.
		* @param  Group's ID.
		* @param  The new group subject.
		* @param  EMError used for output.
		* @return The group that with new subject.
		*/
		EMGroup^ changeGroupSubject(
			const String^ groupId,
			const String^ newSubject,
			EMError^ error
		);

		/**
		* \brief Change group's description.
		*
		* Note: Only group's owner can change group's description.
		* @param  Group's ID.
		* @param  The new group description.
		* @param  EMError used for output.
		* @return The group that with new description.
		*/
		EMGroup^ changeGroupDescription(
			const String^ groupId,
			const String^ newDescription,
			EMError^ error
		);

		/**
		* \brief Get group's specification.
		*
		* @param  Group's ID.
		* @param  EMError used for output.
		* @param  Whether get group's members.
		* @return The group that update it's specification.
		*/
		EMGroup^ fetchGroupSpecification(
			const String^ groupId,
			EMError^ error,
			bool fetchMembers
		);

		/**
		* \brief Search for a public group.
		*
		* @param  Group's ID.
		* @param  EMError used for output.
		* @return The search result.
		*/
		EMGroup^ searchPublicGroup(
			const String^ groupId,
			EMError^ error
		);

		/**
		* \brief Block group message.
		*
		* Note: Owner can't block the group message.
		* @param  Group's ID.
		* @param  EMError used for output.
		* @return The group that blocked message.
		*/
		EMGroup^ blockGroupMessage(
			const String^ groupId,
			EMError^ error
		);

		/**
		* \brief Unblock group message.
		*
		* @param  Group's ID.
		* @param  EMError used for output.
		* @return The group that unclocked message.
		*/
		EMGroup^ unblockGroupMessage(
			const String^ groupId,
			EMError^ error
		);

		/**
		* \brief Accept user's join application.
		*
		* Note: Only group's owner can approval someone's application.
		* @param  Group's ID.
		* @param  The user that send the application.
		* @param  EMError used for output.
		* @return NA.
		*/
		void acceptJoinGroupApplication(
			const String^ groupId,
			const String^ user,
			EMError^ error);

		/**
		* \brief Reject user's join application.
		*
		* Note: Only group's owner can reject someone's application.
		* @param  Group's ID.
		* @param  The user that send the application.
		* @param  The reject reason.
		* @param  EMError used for output.
		* @return NA.
		*/
		void declineJoinGroupApplication(
			const String^ groupId,
			const String^ user,
			const String^ reason,
			EMError^ error);

		/**
		* \brief accept group's invitation.
		*
		* @param  Group's ID.
		* @param  Inviter.
		* @param  EMError used for output.
		* @return The group user has accepted.
		*/
		EMGroup^ acceptInvitationFromGroup(
			const String^ groupId,
			const String^ inviter,
			EMError^ error);

		/**
		* \brief decline group's invitation.
		*
		* @param  Group's ID.
		* @param  Inviter.
		* @param  The decline reason.
		* @param  EMError used for output.
		* @return NA.
		*/
		void declineInvitationFromGroup(
			const String^ groupId,
			const String^ inviter,
			const String^ reason,
			EMError^ error);

	private:
		void* listenerDelegate;
		easemob::EMGroupManagerInterface* getImpl();
		easemob::EMGroupManagerInterface* mImpl;
	};
}

