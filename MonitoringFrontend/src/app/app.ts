import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { DatePipe } from '@angular/common';
import { ApiService } from './services/api.service';
import { DeviceInfo, SessionInfo, SortDirection, DeleteParameters } from './models/models';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [DatePipe],
  templateUrl: './app.html',
  styleUrl: './app.less'
})
export class AppComponent implements OnInit {
  devices: DeviceInfo[] = [];
  selectedSessions: SessionInfo[] = [];

  selectedDeviceId: string | null = null;
  totalDevices = 0;
  totalSessions = 0;

  selectedSessionIds = new Set<string>();

  filter = {
    limit: 10,
    offset: 0,
    sortDirection: SortDirection.Value1
  };

  sessionFilter = {
    limit: 20,
    offset: 0,
    sortDirection: SortDirection.Value1
  };

  constructor(
    private api: ApiService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit() {
    this.loadDevices();
  }


  loadDevices() {
    this.api.getDevices(this.filter.offset, this.filter.limit, this.filter.sortDirection)
      .subscribe({
        next: (response) => {
          this.devices = response?.items || [];
          this.totalDevices = response?.totalCount || 0;
          this.cdr.detectChanges();
        },
        error: (err) => console.error('Ошибка загрузки устройств', err)
      });
  }

  nextPage() {
    this.filter.offset += this.filter.limit;
    this.loadDevices();
  }

  prevPage() {
    if (this.filter.offset > 0) {
      this.filter.offset -= this.filter.limit;
      this.loadDevices();
    }
  }


  onSelectDevice(deviceId: string | undefined) {
    if (!deviceId) return;

    this.selectedDeviceId = deviceId;

    this.selectedSessionIds.clear();
    this.sessionFilter.offset = 0;

    this.cdr.detectChanges();

    this.loadSessions();
  }


  loadSessions() {
    if (!this.selectedDeviceId) return;

    this.api.getSessions(
      this.selectedDeviceId,
      this.sessionFilter.offset,
      this.sessionFilter.limit,
      this.sessionFilter.sortDirection
    ).subscribe({
      next: (response) => {
        this.selectedSessions = response?.items || [];
        this.totalSessions = response?.totalCount || 0;
        this.cdr.detectChanges();
      },
      error: (err) => console.error('Ошибка загрузки сессий', err)
    });
  }

  nextSessionPage() {
    this.sessionFilter.offset += this.sessionFilter.limit;
    this.loadSessions();
  }

  prevSessionPage() {
    if (this.sessionFilter.offset > 0) {
      this.sessionFilter.offset -= this.sessionFilter.limit;
      this.loadSessions();
    }
  }


  toggleSession(sessionId: string) {
    if (this.selectedSessionIds.has(sessionId)) {
      this.selectedSessionIds.delete(sessionId);
    } else {
      this.selectedSessionIds.add(sessionId);
    }
  }

  toggleAllSessions(event: any) {
    const isChecked = event.target.checked;
    if (isChecked) {
      this.selectedSessions.forEach(s => this.selectedSessionIds.add(s.id));
    } else {
      this.selectedSessionIds.clear();
    }
  }

  isSessionSelected(session: SessionInfo): boolean {
    return this.selectedSessionIds.has(session.id);
  }


  deleteData(dateValue: string) {
    const params: DeleteParameters = {
      deviceId: this.selectedDeviceId,
      sessionIds: Array.from(this.selectedSessionIds),
      cleanupDate: dateValue ? new Date(dateValue).toISOString() : null
    };

    if (!params.sessionIds?.length && !params.cleanupDate) {
      alert('Выберите сессии галочками или укажите дату для удаления!');
      return;
    }

    if (!confirm('Вы уверены, что хотите удалить выбранные записи?')) {
      return;
    }

    this.api.deleteSessions(params).subscribe({
      next: () => {
        alert('Успешно удалено');
        this.selectedSessionIds.clear();

        this.loadSessions();
        this.loadDevices();

        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error(err);
        alert('Ошибка удаления: ' + (err.error?.message || err.message));
      }
    });
  }
}
