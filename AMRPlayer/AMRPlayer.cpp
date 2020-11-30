#include "pch.h"
#include <vcclr.h>
#include "AMRPlayer.h"
#include <msclr/marshal.h>

namespace AMRPlayer {
	AMRPlayer::AMRPlayer()
	{
		player = new WavePlayer();
	}
	AMRPlayer::~AMRPlayer()
	{
		if (player != nullptr) {
			if (player->IsPlaying()) {
				player->Stop();
			}
			delete player;
		}
	}
	void AMRPlayer::Play(String^ lpszFile, [Out] UInt32% pLength)
	{
		msclr::interop::marshal_context context;
		LPCTSTR file = context.marshal_as<LPCTSTR>(lpszFile);
		DWORD p;
		player->Play(file, &p);
		pLength = p;
	}
	void AMRPlayer::Pause()
	{
		player->Pause();
	}
	void AMRPlayer::Resume()
	{
		player->Resume();
	}
	void AMRPlayer::Stop()
	{
		player->Stop();
	}
	UInt32 AMRPlayer::GetAudioLength(String^ lpszFile)
	{
		msclr::interop::marshal_context context;
		LPCTSTR file = context.marshal_as<LPCTSTR>(lpszFile);
		return player->GetAudioLength(file);
	}
	void AMRPlayer::SetVolume(UInt32 dwVolume)
	{
		player->SetVolume(dwVolume);
	}
	bool AMRPlayer::IsPlaying()
	{
		return player->IsPlaying();
	}
}