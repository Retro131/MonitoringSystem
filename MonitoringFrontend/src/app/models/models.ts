/* eslint-disable */
/* tslint:disable */
// @ts-nocheck
/*
 * ---------------------------------------------------------------
 * ## THIS FILE WAS GENERATED VIA SWAGGER-TYPESCRIPT-API        ##
 * ##                                                           ##
 * ## AUTHOR: acacode                                           ##
 * ## SOURCE: https://github.com/acacode/swagger-typescript-api ##
 * ---------------------------------------------------------------
 */

/**
 * Направления сортировки
 * @format int32
 */
export enum SortDirection {
  Value0 = 0,
  Value1 = 1,
}

/** Параметры на удаление записей */
export interface DeleteParameters {
  /**
   * Дата, информацию старше которой нужно удалить
   * @format date-time
   */
  cleanupDate?: string | null;
  /**
   * Идентификатор девайса, у которого нужно удалить сессии
   * @format uuid
   */
  deviceId?: string | null;
  /** Список сессий, которые нужно удалить */
  sessionIds?: string[] | null;
}

/** Информация об устройстве */
export interface DeviceInfo {
  /**
   * Идентификатор устройства
   * @format uuid
   */
  id?: string;
  /**
   * Дата появления первой записи об устройстве
   * @format date-time
   */
  createdAt?: string;
  /**
   * Дата последнего использования устройства
   * @format date-time
   */
  lastSeenAt?: string;
  /**
   * Общее количество сессий на устройстве
   * @format int32
   */
  sessionsCount?: number;
}

/** Страница для пагинации */
export interface DeviceInfoPage {
  /** Элементы страницы */
  items?: DeviceInfo[] | null;
  /**
   * Количество элементов на странице
   * @format int32
   */
  totalCount?: number;
}

/** Информация о Сессии */
export interface SessionInfo {
  /**
   * Идентификатор сессии
   * @format uuid
   */
  id: string;
  /**
   * Идентификатор устройства, которому принадлежит данная сессия
   * @format uuid
   */
  deviceId: string;
  /**
   * Имя пользователя
   * @minLength 1
   * @maxLength 256
   */
  name: string;
  /**
   * Начало сессии
   * @format date-time
   */
  startTime: string;
  /**
   * Конец сессии
   * @format date-time
   */
  endTime: string;
  /**
   * Версия, установленного приложения
   * @minLength 1
   */
  version: string;
}

/** Страница для пагинации */
export interface SessionInfoPage {
  /** Элементы страницы */
  items?: SessionInfo[] | null;
  /**
   * Количество элементов на странице
   * @format int32
   */
  totalCount?: number;
}
